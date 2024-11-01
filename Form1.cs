using System.Diagnostics;
using System.Text;
using Octokit;
using HtmlAgilityPack;
using System.Net;
using System.Reflection;
using System.Management;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

// I am so trash at C# please help

namespace ARC_Firmware_Tool
{
    public partial class Form1 : Form
    {

        // Lets do some git config
        private const string RepoOwner = "";
        private const string RepoName = "ARC-Firmware-Tool";
        // Make this expire and make this limited in scope (per repo) then only give it read perms to code meta data. This token should be repo/app specific dont give it yours or your other apps.
        private const string PersonalAccessToken = "";
        // Expire when?: Thu, Aug 19 2025
        private string currentVersion;

        // FTP Configuration
        private const string FtpServerUrl = "ftp://example.com";
        private const string FtpUsername = "my-user";
        private const string FtpPassword = "user-password";
        private const string DownloadBaseUrl = "https://example.com/yourdirectory/";

        // Certificate validation configuration This is the SHA-256 Certificate fingerprint.
        // Fingerprints can change everytime the certificate is renewed, so we need to update this value accordingly
        private const string KnownGoodThumbprint = "your known good thumbprint here";
        private const string ExpectedHostname = "example.com";

        public Form1()
        {
            InitializeComponent();

            // About box event handler
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            // Save log handler
            saveTextToolStripMenuItem.Click += saveTextToolStripMenuItem_Click;

            // Download vbios handler
            downloadLatestToolStripMenuItem.Click += downloadLatestToolStripMenuItem_Click;

            // Trigger IGSC manually
            manualToolStripMenuItem.Click += new EventHandler(manualToolStripMenuItem_Click);

            // Trigger Intel-API manually
            aPIDebugToolStripMenuItem.Click += new EventHandler(aPIDebugToolStripMenuItem_Click);

            // Initialize currentVersion
            InitializeCurrentVersion();

            // FTP upload event handler
            this.uploadLogToolStripMenuItem.Click += new EventHandler(uploadLogToolStripMenuItem_Click);

            // Link click event handler
            richTextBox1.LinkClicked += new LinkClickedEventHandler(RichTextBox1_LinkClicked);

            // Extract Intel files to temp folder once at startup
            ExtractIntelFilesToTemp();
        }

        // Extracts embedded resources to a temporary folder
        private void ExtractIntelFilesToTemp()
        {
            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            String myProject = "ARC_Firmware_Tool";
            String[] files = { "igsc.exe", "igsc.dll", "Intel-API.exe" };
            String outputPath = System.IO.Path.GetTempPath();

            foreach (string fileName in files)
            {
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + fileName))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(Path.Combine(outputPath, fileName), System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }
            }
        }

        // Get the current version from the build instead of defining it manually
        private void InitializeCurrentVersion()
        {
            // Get the assembly version of the current application
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Retrieve the product version from the assembly
            string productVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            // Use productVersion as the current version if available, otherwise use the assembly version
            currentVersion = !string.IsNullOrWhiteSpace(productVersion) ? productVersion : assembly.GetName().Version.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        // BEGIN Alchemist Block

        // Buttons here

        // Firmware Browse buttons
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg1 = new OpenFileDialog();
            fdlg1.Title = "Select Firmware Bin/Rom";
            fdlg1.InitialDirectory = @"c:\";
            fdlg1.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg1.FilterIndex = 2;
            fdlg1.RestoreDirectory = true;
            if (fdlg1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg1.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg2 = new OpenFileDialog();
            fdlg2.Title = "Select Oprom (Data) Bin/Rom";
            fdlg2.InitialDirectory = @"c:\";
            fdlg2.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg2.FilterIndex = 2;
            fdlg2.RestoreDirectory = true;
            if (fdlg2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fdlg2.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg3 = new OpenFileDialog();
            fdlg3.Title = "Select Oprom (Code) Bin/Rom";
            fdlg3.InitialDirectory = @"c:\";
            fdlg3.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg3.FilterIndex = 2;
            fdlg3.RestoreDirectory = true;
            if (fdlg3.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = fdlg3.FileName;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg4 = new OpenFileDialog();
            fdlg4.Title = "Select Firmware (Data/Config) Bin/Rom";
            fdlg4.InitialDirectory = @"c:\";
            fdlg4.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg4.FilterIndex = 2;
            fdlg4.RestoreDirectory = true;
            if (fdlg4.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = fdlg4.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg5 = new OpenFileDialog();
            fdlg5.Title = "Choose Bin/Rom to check";
            fdlg5.InitialDirectory = @"c:\";
            fdlg5.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg5.FilterIndex = 2;
            fdlg5.RestoreDirectory = true;
            if (fdlg5.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = fdlg5.FileName;
            }
        }

        // Check FW Button
        private async void button9_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Display the introductory message before the process output
            AppendTextToRichTextBox(richTextBox1, "Checking selected file...\n");

            await Task.Run(async () =>
            {
                String outputPath = System.IO.Path.GetTempPath();
                String file1 = "igsc.exe";
                String executablePath = Path.Combine(outputPath, file1);

                // Create variables from other methods
                string fdlg5 = textBox5.Text;

                // FW File Copies
                if (!string.IsNullOrEmpty(fdlg5))
                {
                    File.Copy(fdlg5, Path.Combine(outputPath, Path.GetFileName(fdlg5)), true);
                }

                AppendTextToRichTextBox(richTextBox1, $"Checking file:\n \"{fdlg5}\"\n");
                AppendTextToRichTextBox(richTextBox1, "Checking if we can identify this image type...\n");
                await RunProcessWithOutputAsync($"image-type -i \"{fdlg5}\"", executablePath, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if FW...\n");
                await RunProcessWithOutputAsync($"fw version -i \"{fdlg5}\"", executablePath, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if Oprom-Data...\n");
                await RunProcessWithOutputAsync($"oprom-data version -i \"{fdlg5}\"", executablePath, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if Oprom-Code...\n");
                await RunProcessWithOutputAsync($"oprom-code version -i \"{fdlg5}\"", executablePath, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if FW-Data...\n");
                await RunProcessWithOutputAsync($"fw-data version -i \"{fdlg5}\"", executablePath, outputPath);

                AppendTextToRichTextBox(richTextBox1, "\nFinished checking file!");
            });
        }

        // Made this async so I can add more later.
        // Scan Hardware Button
        private async void button1_Click(object sender, EventArgs e)
        {
            await PerformHardwareScanAsync();
        }

        private async Task PerformHardwareScanAsync()
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Stamp date and tool version
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dddd, MMMM dd yyyy hh:mm tt\n");
            AppendTextToRichTextBox(richTextBox1, $"Current date and time: " + formattedDateTime);
            AppendTextToRichTextBox(richTextBox1, $"ARC Firmware Tool Version: {currentVersion}{Environment.NewLine}");

            // Get GPU driver version
            string deviceToSearch = "Intel(R) Arc(TM)";
            HashSet<string> displayedDriverVersions = new HashSet<string>();

            await Task.Run(async () =>
            {
                String outputPath = System.IO.Path.GetTempPath();
                String file1 = "igsc.exe";
                String executablePath = Path.Combine(outputPath, file1);

                // Retrieve unique driver versions
                string query = $"SELECT DeviceName, Manufacturer, DriverVersion FROM Win32_PnPSignedDriver WHERE DeviceName LIKE '%{deviceToSearch}%'";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject driver in searcher.Get())
                {
                    string driverVersion = driver["DriverVersion"].ToString();

                    if (displayedDriverVersions.Add(driverVersion))
                    {
                        AppendTextToRichTextBox(richTextBox1, $"Installed GPU driver version: {driverVersion}\n");
                    }
                }

                // Retrieve GOP versions along with adapter names using Intel-API.exe
                List<(string AdapterName, string GopVersion)> adapterInfos = await GetGopVersionsAsync(outputPath);

                if (adapterInfos.Count == 0)
                {
                    AppendTextToRichTextBox(richTextBox1, "No adapters detected or unable to retrieve GOP versions.\n");
                    AppendTextToRichTextBox(richTextBox1, "If this is unexpected check device manager for the HECI device or do a clean install of the drivers.\n", bold: true);
                }
                else
                {
                    foreach (var adapterInfo in adapterInfos)
                    {
                        string message = $"Adapter Name: {adapterInfo.AdapterName}\nGOP (vBIOS) Version: {adapterInfo.GopVersion}";

                        if (adapterInfo.GopVersion == "0.0.0")
                        {
                            message += " (GPU may not be initialized/active)";
                        }

                        message += "\n\n"; // Adding extra newline for spacing between adapters

                        AppendTextToRichTextBox(richTextBox1, message);
                    }
                }

                // Call igsc commands with modified device list handling
                AppendTextToRichTextBox(richTextBox1, "Listing Device and FW/Oprom Versions:\n");
                await CaptureDeviceListWithSpacingAsync(executablePath, outputPath, "list-devices -i", adapterInfos);

                AppendTextToRichTextBox(richTextBox1, "\nThe following is only applicable to Device [1]\n\n", bold: true);
                AppendTextToRichTextBox(richTextBox1, "Listing Devices HW Config:\n");
                await RunProcessWithOutputAsync("fw hwconfig", executablePath, outputPath);

                AppendTextToRichTextBox(richTextBox1, "Listing FW Data and FW Code Versions:\n");
                await RunProcessWithOutputAsync("fw-data version", executablePath, outputPath);

                AppendTextToRichTextBox(richTextBox1, "Listing OEM FW Version:\n");
                await RunProcessWithOutputAsync("oem version", executablePath, outputPath);

                AppendTextToRichTextBox(richTextBox1, "\nFinished scanning hardware.");
            });
        }

        // Capture device list with added spacing and card name
        private async Task CaptureDeviceListWithSpacingAsync(string executablePath, string outputPath, string arguments, List<(string AdapterName, string GopVersion)> adapterInfos)
        {
            await Task.Run(() =>
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = executablePath;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    int deviceIndex = 0;
                    bool isFirstDevice = true;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            // Display card name before each device's details
                            if (e.Data.Contains("Device ["))
                            {
                                if (!isFirstDevice)
                                {
                                    AppendTextToRichTextBox(richTextBox1, Environment.NewLine);
                                }

                                // Display associated adapter name in parentheses
                                if (deviceIndex < adapterInfos.Count)
                                {
                                    AppendTextToRichTextBox(richTextBox1, $"({adapterInfos[deviceIndex].AdapterName})\n");
                                    deviceIndex++;
                                }
                                isFirstDevice = false;
                            }

                            // Display device information
                            AppendTextToRichTextBox(richTextBox1, e.Data + Environment.NewLine);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }
            });
        }

        // Capture device list with added spacing between devices
        private async Task CaptureDeviceListWithSpacingAsync(string executablePath, string outputPath, string arguments)
        {
            await Task.Run(() =>
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = executablePath;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    bool isFirstDevice = true;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            if (e.Data.Contains("Device [") && !isFirstDevice)
                            {
                                AppendTextToRichTextBox(richTextBox1, Environment.NewLine); // Add space between devices
                            }
                            AppendTextToRichTextBox(richTextBox1, e.Data + Environment.NewLine);
                            isFirstDevice = false;
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }
            });
        }

        // Add Intel API Methods here:

        // Method to get the GOP version along with adapter names
        private async Task<List<(string AdapterName, string GopVersion)>> GetGopVersionsAsync(string outputPath)
        {
            List<(string AdapterName, string GopVersion)> adapterInfo = new List<(string AdapterName, string GopVersion)>();
            string intelApiPath = Path.Combine(outputPath, "Intel-API.exe");

            if (!File.Exists(intelApiPath))
            {
                AppendTextToRichTextBox(richTextBox1, "Error: Intel-API.exe not found.\n");
                return adapterInfo;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = intelApiPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                if (process == null || process.StandardOutput == null)
                {
                    AppendTextToRichTextBox(richTextBox1, "Error: Failed to start Intel-API.exe process.\n");
                    return adapterInfo;
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = await reader.ReadToEndAsync();
                    string[] lines = result.Split('\n');

                    string currentAdapterName = "";
                    string currentGopVersion = "";
                    foreach (var line in lines)
                    {
                        if (line.Contains("Intel Adapter Name:"))
                        {
                            currentAdapterName = line.Split(':')[1].Trim();
                            if (!string.IsNullOrEmpty(currentGopVersion))
                            {
                                adapterInfo.Add((currentAdapterName, currentGopVersion));
                                currentGopVersion = ""; // Reset for the next adapter
                            }
                        }
                        else if (line.Contains("GOP Version :"))
                        {
                            currentGopVersion = line.Split(':')[1].Trim();
                        }
                    }
                }
            }

            return adapterInfo;
        }

        // Flash Button
        private async void button3_Click(object sender, EventArgs e)
        {
            // Begin flash process

            // Disable buttons
            button3.Enabled = false;
            button2.Enabled = false;

            // Clear the RichTextBox
            richTextBox1.Clear();

            // Display the introductory message before the process output
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dddd, MMMM dd yyyy hh:mm tt\n");
            AppendTextToRichTextBox(richTextBox1, $"Current date and time: " + formattedDateTime);
            AppendTextToRichTextBox(richTextBox1, $"ARC Firmware Tool Version: {currentVersion}{Environment.NewLine}");
            AppendTextToRichTextBox(richTextBox1, "Now Flashing...\nDo not close program while flashing is in progress!\n");

            String outputPath = System.IO.Path.GetTempPath();
            String file1 = "igsc.exe";
            String executablePath = Path.Combine(outputPath, file1);

            // Create variables from other methods
            string fdlg1 = textBox1.Text;
            string fdlg2 = textBox2.Text;
            string fdlg3 = textBox3.Text;
            string fdlg4 = textBox4.Text;

            // FW File Copies
            if (!string.IsNullOrEmpty(fdlg1))
            {
                File.Copy(fdlg1, Path.Combine(outputPath, Path.GetFileName(fdlg1)), true);
            }
            if (!string.IsNullOrEmpty(fdlg2))
            {
                File.Copy(fdlg2, Path.Combine(outputPath, Path.GetFileName(fdlg2)), true);
            }
            if (!string.IsNullOrEmpty(fdlg3))
            {
                File.Copy(fdlg3, Path.Combine(outputPath, Path.GetFileName(fdlg3)), true);
            }
            if (!string.IsNullOrEmpty(fdlg4))
            {
                File.Copy(fdlg4, Path.Combine(outputPath, Path.GetFileName(fdlg4)), true);
            }

            await RunProcessesAsync(executablePath, fdlg1, fdlg2, fdlg3, fdlg4, outputPath);

            AppendTextToRichTextBox(richTextBox1, "Flashing complete!");

            // Re-Enable buttons
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private async Task RunProcessesAsync(string executablePath, string fdlg1, string fdlg2, string fdlg3, string fdlg4, string outputPath)
        {
            // I should bring back the checkboxes but igsc has issues when I pass "null" values when a checkbox variable is empty.
            // Maybe I can do some kind of truncating so it doesnt show as a space but its easier to just auto force and auto allow downgrade.
            await Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(fdlg1))
                {
                    AppendTextToRichTextBox(richTextBox1, $"Flashing FW File:\n \"{fdlg1}\"\n");
                    await RunProcessWithOutputAsync($"fw update -a -f -i \"{fdlg1}\"", executablePath, outputPath);
                }
                if (!string.IsNullOrEmpty(fdlg2))
                {
                    AppendTextToRichTextBox(richTextBox1, $"Flashing Oprom Data File:\n \"{fdlg2}\"\n");
                    await RunProcessWithOutputAsync($"oprom-data update -a -i \"{fdlg2}\"", executablePath, outputPath);
                }
                if (!string.IsNullOrEmpty(fdlg3))
                {
                    AppendTextToRichTextBox(richTextBox1, $"Flashing Oprom Code File:\n \"{fdlg3}\"\n");
                    await RunProcessWithOutputAsync($"oprom-code update -a -i \"{fdlg3}\"", executablePath, outputPath);
                }
                if (!string.IsNullOrEmpty(fdlg4))
                {
                    AppendTextToRichTextBox(richTextBox1, $"Flashing FW Data File:\n \"{fdlg4}\"\n");
                    await RunProcessWithOutputAsync($"fw-data update -a -i \"{fdlg4}\"", executablePath, outputPath);
                }
            });
        }

        private async Task RunProcessWithOutputAsync(string arguments, string executableFileName, string outputPath)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = executableFileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                // Start tasks to read output and error streams
                Task outputTask = Task.Run(() => ReadStream(process.StandardOutput.BaseStream, richTextBox1));
                Task errorTask = Task.Run(() => ReadStream(process.StandardError.BaseStream, richTextBox1));

                await Task.WhenAll(outputTask, errorTask, process.WaitForExitAsync());
            }
        }

        // Class-level variables
        private int progressLineStartIndex = -1;
        private string lastProgressLine = string.Empty;
        private string lastProgressPercentage = string.Empty;
        private DateTime lastProgressUpdate = DateTime.MinValue;

        // Read strem from IGSC
        private void ReadStream(Stream stream, RichTextBox richTextBox)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                int cInt;
                StringBuilder lineBuffer = new StringBuilder();

                while ((cInt = reader.Read()) != -1)
                {
                    char c = (char)cInt;

                    if (c == '\r')
                    {
                        // Carriage return; process the line as a progress line
                        string line = lineBuffer.ToString().TrimEnd();
                        lineBuffer.Clear();

                        ProcessLine(line, richTextBox, isProgressLine: true);
                    }
                    else if (c == '\n')
                    {
                        // Newline; process the line as a regular line
                        string line = lineBuffer.ToString().TrimEnd();
                        lineBuffer.Clear();

                        ProcessLine(line, richTextBox, isProgressLine: false);
                    }
                    else
                    {
                        lineBuffer.Append(c);
                    }
                }

                // Append any remaining text
                if (lineBuffer.Length > 0)
                {
                    string line = lineBuffer.ToString().TrimEnd();
                    ProcessLine(line, richTextBox, isProgressLine: false);
                }

                // Reset progress tracking variables
                progressLineStartIndex = -1;
                lastProgressLine = string.Empty;
                lastProgressPercentage = string.Empty;
            }
        }

        // Process the text returned so we can make it more readable
        private void ProcessLine(string line, RichTextBox richTextBox, bool isProgressLine)
        {
            if (isProgressLine && IsProgressLine(line))
            {
                // Handle progress line with throttling and reformatting
                string formattedLine = FormatProgressLine(line);
                string percentage = ExtractPercentage(formattedLine);

                if (percentage != lastProgressPercentage)
                {
                    lastProgressPercentage = percentage;
                    ReplaceProgressLineInRichTextBox(richTextBox, formattedLine);
                }
                // If the percentage hasn't changed, do nothing
            }
            else
            {
                // Non-progress line
                if (!string.IsNullOrWhiteSpace(line))
                {
                    AppendTextToRichTextBox(richTextBox, line);

                    // Insert extra line break after specific lines
                    if (line.StartsWith("Image:  FW Version:") || line.StartsWith("Device: FW Version:"))
                    {
                        AppendTextToRichTextBox(richTextBox, ""); // Append an empty line
                    }
                }

                progressLineStartIndex = -1; // Reset progress line index after a non-progress line
            }
        }

        // Search for the output we expect from the flash process
        private bool IsProgressLine(string line)
        {
            // Use a regular expression to identify progress lines
            // Expected format: "Progress X/100:XX%"
            return Regex.IsMatch(line, @"^Progress\s+\d+/\d+:\s*\d+%$");
        }

        // Format our captured text
        private string FormatProgressLine(string line)
        {
            // Use regular expression to extract the percentage
            var match = Regex.Match(line, @"^Progress\s+\d+/\d+:\s*(\d+%)$");
            if (match.Success)
            {
                string percentage = match.Groups[1].Value;
                return $"Progress: {percentage}";
            }
            // If not a progress line, return the original line
            return line;
        }

        // Get the percentage value so we can use it
        private string ExtractPercentage(string line)
        {
            // Extract the percentage value from the formatted line "Progress: XX%"
            var match = Regex.Match(line, @"^Progress:\s*(\d+%)$");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }

        private void ReplaceProgressLineInRichTextBox(RichTextBox richTextBox, string text)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() => ReplaceProgressLineInRichTextBox(richTextBox, text)));
            }
            else
            {
                if (progressLineStartIndex < 0)
                {
                    // Progress line hasn't been set yet; append the text and set the start index
                    if (richTextBox.TextLength > 0 && richTextBox.Text[richTextBox.TextLength - 1] != '\n')
                    {
                        // Ensure there's a newline before the progress line
                        richTextBox.AppendText(Environment.NewLine);
                    }

                    progressLineStartIndex = richTextBox.TextLength;
                    richTextBox.AppendText(text);
                }
                else
                {
                    // Replace the text starting from progressLineStartIndex
                    int lengthToReplace = richTextBox.TextLength - progressLineStartIndex;
                    richTextBox.Select(progressLineStartIndex, lengthToReplace);
                    richTextBox.SelectedText = text;
                }
            }
        }

        // The text box
        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text, bool bold = false)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() => AppendTextToRichTextBox(richTextBox, text, bold)));
            }
            else
            {
                // Ensure new line if needed
                if (richTextBox.TextLength > 0 && richTextBox.Text[richTextBox.TextLength - 1] != '\n')
                {
                    richTextBox.AppendText(Environment.NewLine);
                }

                // Apply bold font if requested
                if (bold)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font, FontStyle.Bold);
                }

                // Append the text and add a new line
                richTextBox.AppendText(text + Environment.NewLine);

                // Reset font to default
                richTextBox.SelectionFont = richTextBox.Font;
            }
        }

        // END Alchemist Block



        // Begin System Block

        // About box
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        // Lets get the latest BIOS'
        private async void downloadLatestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Tell them we are checking since this can take some time.
            AppendTextToRichTextBox(richTextBox1, "Checking for vBIOS Package...");

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    string url = "https://teamdotexe.org/downloads/Intel-FW/Latest/Latest.zip";  // Set file url

                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = "Latest_Intel_BIOS.zip";  // Set the default file name and extension
                        saveFileDialog.Filter = "Zip Files (*.zip)|*.zip|All Files (*.*)|*.*"; // Set the default file extension filter
                        saveFileDialog.Title = "Save Latest Bios Pack"; // Set the window title here
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;

                            using (var stream = await response.Content.ReadAsStreamAsync())
                            using (var fileStream = new FileStream(filePath, System.IO.FileMode.Create))  // Specify System.IO.FileMode or it gets mad about my other filepath call
                            {
                                await stream.CopyToAsync(fileStream);
                            }

                            MessageBox.Show("Files downloaded successfully!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error downloading the file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: \n(Do you have internet?) \n" + ex.Message, "Download Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Let's download the latest driver
        private async void downloadDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Inform the user that the check is in progress
            AppendTextToRichTextBox(richTextBox1, "Checking for new Driver...");

            try
            {
                // Load the page from the new URL
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument document = web.Load("https://teamdotexe.org/downloads/Intel-Driver/");

                // Find the first .exe link from the page
                var linkNode = document.DocumentNode.SelectSingleNode("//a[contains(@href, '.exe')]");

                if (linkNode == null)
                {
                    MessageBox.Show("Could not find a driver to download.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Send the page URL to the text box if no link is found
                    AppendTextToRichTextBox(richTextBox1, "\nNo driver found. Check manually:\n\nhttps://teamdotexe.org/downloads/Intel-Driver/\nor\nhttps://www.intel.com/content/www/us/en/download/785597/intel-arc-iris-xe-graphics-windows.html");
                    return;
                }

                string downloadUrl = "https://teamdotexe.org/downloads/Intel-Driver/" + linkNode.GetAttributeValue("href", "");
                string fileName = linkNode.InnerText.Trim();

                // Prompt the user for a save location
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Exe Files (*.exe)|*.exe|All Files (*.*)|*.*";
                saveFileDialog.FileName = fileName;
                saveFileDialog.Title = "Save Driver"; // Set the window title here

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;

                    // Use Task.Run to download the file on a separate thread
                    await Task.Run(() =>
                    {
                        using (WebClient client = new WebClient())
                        {
                            // Handle the DownloadProgressChanged event to update the download status
                            client.DownloadProgressChanged += (s, args) =>
                            {
                                int progressPercentage = args.ProgressPercentage;
                                string progressText = $"Downloading: {progressPercentage}%";

                                richTextBox1.BeginInvoke(new Action(() =>
                                {
                                    richTextBox1.Text = progressText;
                                }));
                            };

                            // Handle the DownloadFileCompleted event to display a completion message
                            client.DownloadFileCompleted += (s, args) =>
                            {
                                if (args.Error != null)
                                {
                                    MessageBox.Show("Error downloading file: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    MessageBox.Show("Download completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            };

                            try
                            {
                                // Start the download asynchronously
                                client.DownloadFileAsync(new Uri(downloadUrl), savePath);

                                // Notify the user that the download has started
                                MessageBox.Show("Download started.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error downloading file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lets do a software update (Stable)
        // Do some version checks
        // Maybe make a temp process to close and open the new version later but file handling is hard and I hate it
        private async void stableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Tell them we are checking since this can take some time.
            AppendTextToRichTextBox(richTextBox1, "Checking for new version (Stable)...");

            try
            {
                bool updateNeeded = await CheckAndUpdateVersionAsync();

                if (updateNeeded)
                {
                    var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));
                    github.Credentials = new Credentials(PersonalAccessToken);

                    // Get the latest release tag name
                    var latestRelease = await github.Repository.Release.GetLatest(RepoOwner, RepoName);
                    string tagName = latestRelease?.TagName ?? "latest";

                    var saveFileDialog = new SaveFileDialog
                    {
                        FileName = $"ARC Firmware Tool {tagName}.exe",
                        Filter = "Executable files (*.exe)|*.exe",
                        Title = "Save New Version"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        await DownloadAndSaveUpdate(saveFileDialog.FileName);
                        MessageBox.Show("Update downloaded!\n\nPlease relaunch new version.", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You're already up to date!", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: \n(Do you have internet?) \n" + ex.Message, "Update Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CheckAndUpdateVersionAsync()
        {
            var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));

            github.Credentials = new Credentials(PersonalAccessToken);

            // Get the latest release instead of pulling the first (which could be beta)
            var latestRelease = await github.Repository.Release.GetLatest(RepoOwner, RepoName);

            if (latestRelease != null)
            {
                string latestVersion = latestRelease.TagName;

                if (Version.TryParse(latestVersion, out Version latest) &&
                    Version.TryParse(currentVersion, out Version current) &&
                    latest > current)
                {
                    return true; // New version is needed
                }
            }
            return false; // App is up to date
        }

        private async Task DownloadAndSaveUpdate(string savePath)
        {
            var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));

            github.Credentials = new Credentials(PersonalAccessToken);

            // Get the latest release
            var latestRelease = await github.Repository.Release.GetLatest(RepoOwner, RepoName);

            if (latestRelease != null)
            {
                var asset = latestRelease.Assets.FirstOrDefault();
                if (asset != null)
                {
                    string assetUrl = asset.BrowserDownloadUrl;

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(assetUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            using (var fileStream = new FileStream(savePath, System.IO.FileMode.Create))
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
            }
        }

        // Lets do a software update (Beta)
        // Do some version checks
        // Maybe make a temp process to close and open the new version later but file handling is hard and I hate it
        private async void betaUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Tell them we are checking since this can take some time.
            AppendTextToRichTextBox(richTextBox1, "Checking for new version (Beta)...");

            try
            {
                bool updateNeeded = await CheckAndBetaUpdateVersionAsync();

                if (updateNeeded)
                {

                    var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));
                    github.Credentials = new Credentials(PersonalAccessToken);

                    // Get the latest tag name regardless
                    var releases = await github.Repository.Release.GetAll(RepoOwner, RepoName);
                    var mostRecentRelease = releases.FirstOrDefault();
                    string tagName = mostRecentRelease?.TagName ?? "latest";

                    var saveFileDialog = new SaveFileDialog
                    {
                        FileName = $"ARC Firmware Tool {tagName}.exe",
                        Filter = "Executable files (*.exe)|*.exe",
                        Title = "Save New Version"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        await DownloadAndSaveBetaUpdate(saveFileDialog.FileName);
                        MessageBox.Show("Update downloaded!\n\nPlease relaunch new version.", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You're already up to date!", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: \n(Do you have internet?) \n" + ex.Message, "Update Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CheckAndBetaUpdateVersionAsync()
        {
            var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));

            github.Credentials = new Credentials(PersonalAccessToken);

            var releases = await github.Repository.Release.GetAll(RepoOwner, RepoName);

            var latestRelease = releases.FirstOrDefault();
            if (latestRelease != null)
            {
                string latestVersion = latestRelease.TagName;

                if (Version.TryParse(latestVersion, out Version latest) &&
                    Version.TryParse(currentVersion, out Version current) &&
                    latest > current)
                {
                    return true; // New version is needed
                }
            }
            return false; // App is up to date
        }

        private async Task DownloadAndSaveBetaUpdate(string savePath)
        {
            var github = new GitHubClient(new ProductHeaderValue("ARC_Firmware_Tool"));

            github.Credentials = new Credentials(PersonalAccessToken);

            var releases = await github.Repository.Release.GetAll(RepoOwner, RepoName);

            var latestRelease = releases.FirstOrDefault();
            if (latestRelease != null)
            {
                var asset = latestRelease.Assets.FirstOrDefault();
                if (asset != null)
                {
                    string assetUrl = asset.BrowserDownloadUrl;

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(assetUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            using (var stream = await response.Content.ReadAsStreamAsync())
                            using (var fileStream = new FileStream(savePath, System.IO.FileMode.Create))
                            {
                                await stream.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
            }
        }

        // Lets save the output from the textbox
        private void saveTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Append time
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("MM-dd-yyyy");

            // Do file save
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save ARC Log"; // Set the window title here
            saveFileDialog.FileName = $"ARC log " + formattedDateTime;  // Set the default file name and extension
            saveFileDialog.Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the text from the RichTextBox
                string textToSave = richTextBox1.Text;

                // Save the text to the selected file
                File.WriteAllText(saveFileDialog.FileName, textToSave);

                MessageBox.Show("Log saved to file.", "Save Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Lets upload the output from the textbox
        private async void uploadLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirmation dialog with sentences on separate lines
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to upload your log?\n\nThis is not reversible.\n\nPress the \"Scan HW\" button to see what will be uploaded.", "Upload HW Scan Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                // Await the hardware scan
                await PerformHardwareScanAsync();

                // Then upload the content of richTextBox1
                UploadRichTextBoxContentToFtp();
            }
            else
            {
                // User chose not to upload, display the message in richTextBox1 so they know
                richTextBox1.Clear();
                richTextBox1.AppendText("Upload canceled by user.");
            }
        }

        // Method to generate a random string so that we dont collide
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                      .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Do some formatting and set variables
        private void UploadRichTextBoxContentToFtp()
        {
            string textToUpload = richTextBox1.Text;

            // Generate a random alphanumeric string of 12 characters then append it
            string randomString = GenerateRandomString(12);
            string fileName = $"ARC-log-{randomString}.txt";
            string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            File.WriteAllText(tempFilePath, textToUpload);

            // Attempt to upload the file and check if it succeeds
            bool uploadSuccess = UploadFileToFtp(FtpServerUrl, tempFilePath, FtpUsername, FtpPassword);

            if (uploadSuccess)
            {
                // Create the download link
                string downloadLink = $"{DownloadBaseUrl}{fileName}";

                // Clear the richTextBox1 and print the download link so they can copy it
                richTextBox1.Clear();
                richTextBox1.AppendText("File uploaded successfully.\n\nYou can copy the following link here:\n\n");
                richTextBox1.AppendText(downloadLink);
                richTextBox1.DetectUrls = true;
            }
            else
            {
                // Upload failed
                richTextBox1.Clear();
                richTextBox1.AppendText("File upload failed.\n\nPlease check your connection and try again.\n\nYou may need to update AFT\n\n");
            }
        }

        // Directly uploads the output from the textbox
        private void uploadLogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Confirm the upload
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to upload your log?\n\nThis action is not reversible.", "Upload Log Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                // Upload the content of richTextBox1 directly without running any scans
                UploadRichTextBoxContentToFtp();
            }
            else
            {
                // User canceled the upload, show the message in richTextBox1
                richTextBox1.Clear();
                richTextBox1.AppendText("Upload canceled by user.");
            }
        }

        // Make link clickable
        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = e.LinkText,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Upload the file to the FTP server
        // Make sure the server config is safe
        // Write some messages to console so you can see what's going on
        // Man I should really tie some of this in via another form
        private bool UploadFileToFtp(string url, string filePath, string username, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url + "/" + Path.GetFileName(filePath));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.EnableSsl = true; // If using FTPS
                request.UseBinary = true;
                request.UsePassive = true;

                byte[] fileContents = File.ReadAllBytes(filePath);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    System.Diagnostics.Debug.WriteLine($"FTP Response Status: {response.StatusCode}, Description: {response.StatusDescription}");

                    if (response.StatusCode == FtpStatusCode.ClosingData ||
                        response.StatusCode == FtpStatusCode.CommandOK ||
                        response.StatusCode == FtpStatusCode.FileActionOK ||
                        response.StatusCode == FtpStatusCode.ClosingControl)
                    {
                        return true; // Assume success if no exception is thrown
                    }
                    else
                    {
                        return false; // Consider other statuses as failures
                    }
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    System.Diagnostics.Debug.WriteLine($"FTP WebException: {response.StatusDescription}");
                    response.Close();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"FTP Exception: {ex.Message}");
                }
                return false; // Treat exceptions as failures
            }
        }

        // Certificate validation method
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            X509Certificate2 cert2 = new X509Certificate2(certificate);

            // Check the expiry date
            if (DateTime.Now > cert2.NotAfter || DateTime.Now < cert2.NotBefore)
            {
                Console.WriteLine("Certificate expired or not yet valid.");
                return false;
            }

            // Check the SHA-256 fingerprint (thumbprint)
            if (cert2.Thumbprint != KnownGoodThumbprint)
            {
                Console.WriteLine("Certificate thumbprint mismatch.");
                return false;
            }

            // Check hostname
            if (!cert2.Subject.Contains($"CN={ExpectedHostname}"))
            {
                Console.WriteLine("Hostname mismatch.");
                return false;
            }

            // Additional checks maybe?

            return true;
        }

        // Trigger IGSC manually
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outputPath = Path.GetTempPath();
            string igscPath = Path.Combine(outputPath, "igsc.exe");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                // Using /K instead of /C so the cmd window stays open after the command is executed
                // Since IGSC is a console app this appears to create a new cmd window for the IGSC output on exit
                Arguments = $"/K \"{igscPath}\" -v",
                UseShellExecute = true,
                CreateNoWindow = false,
                WorkingDirectory = outputPath
            };

            Process.Start(startInfo);
        }

        // API Debug
        // Trigger Intel-API manually
        private async void aPIDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outputPath = Path.GetTempPath();
            string intelApiPath = Path.Combine(outputPath, "Intel-API.exe");

            await RunAPIFromTemp(intelApiPath);
        }

        private async Task RunAPIFromTemp(string intelApiPath)
        {
            if (File.Exists(intelApiPath))
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = intelApiPath;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            AppendTextToRichTextBox(richTextBox1, e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    await process.WaitForExitAsync();
                }
            }
            else
            {
                AppendTextToRichTextBox(richTextBox1, "Error: Intel-API.exe not found.");
            }
        }

        // Leave main form close enabled but ask.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                // Perform cleanup here if needed
                CleanupFiles();
                base.OnFormClosing(e);
                return;
            }

            // Confirm user wants to close
            DialogResult result = MessageBox.Show(this, "Are you sure you want to close?\n\nThis is dangerous if you are flashing!", "Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                // User confirmed they want to exit
                // Perform cleanup here
                // Pay attention to order of operations
                CleanupFiles();
                base.OnFormClosing(e);
            }
        }

        // Method supporting the delete file operation for cleanups on exit.
        private void DeleteFileOrFilesByPattern(string directoryPath, string pattern)
        {
            // Use the pattern to find files
            string[] filesToDelete = Directory.GetFiles(directoryPath, pattern);
            foreach (string file in filesToDelete)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error deleting file '{file}': {ex.Message}");
                }
            }
        }

        // Cleanup method called upon exit
        private void CleanupFiles()
        {
            string outputPath = Path.GetTempPath();

            // Define the file extensions and specific file names to delete
            string[] extensionsAndFilesToDelete = { "*.bin", "*.rom", "igsc.dll", "igsc.exe", "Intel-API.exe", "ARC-Flash-log*.txt", "ARC-log*.txt" };

            // Delete files by pattern
            foreach (string itemToDelete in extensionsAndFilesToDelete)
            {
                DeleteFileOrFilesByPattern(outputPath, itemToDelete);
            }
        }

        // Exit button
        private void button2_Click(object sender, EventArgs e)
        {
            // Simulate form closing to trigger the OnFormClosing event
            this.Close();
        }

        // END System Block
    }
}
