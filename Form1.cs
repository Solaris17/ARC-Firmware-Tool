using System.Diagnostics;
using System.Text;
using Octokit;
using HtmlAgilityPack;
using System.Net;
using System.Reflection;
using System.Management;

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
        // Expire when?: Thu, Aug 22 2024
        private string currentVersion;

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
            fdlg1.Title = "Open File Dialog";
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
            fdlg2.Title = "Open File Dialog";
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
            fdlg3.Title = "Open File Dialog";
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
            fdlg4.Title = "Open File Dialog";
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
            fdlg5.Title = "Open File Dialog";
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
                // Read the resource files and copy them out.
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

                String myProject = "ARC_Firmware_Tool";
                String file1 = "igsc.exe";
                String file2 = "igsc.dll";
                String file3 = "Intel-API.exe";
                String outputPath = System.IO.Path.GetTempPath();
                String executablePath = Path.Combine(outputPath, file1);

                // Create variables from other methods
                string fdlg5 = textBox5.Text;

                // First file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file1))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file1, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // Next file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file2))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file2, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // Third file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file3))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file3, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // FW File Copies
                if (!string.IsNullOrEmpty(fdlg5))
                {
                    File.Copy(fdlg5, Path.Combine(outputPath, Path.GetFileName(fdlg5)), true);
                }

                AppendTextToRichTextBox(richTextBox1, $"Checking file:\n \"{fdlg5}\"\n");
                AppendTextToRichTextBox(richTextBox1, "Checking if we can identify this image type...\n");
                await RunProcessWithOutputAsync($"image-type -i \"{fdlg5}\"", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if FW...\n");
                await RunProcessWithOutputAsync($"fw version -i \"{fdlg5}\"", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if Oprom-Data...\n");
                await RunProcessWithOutputAsync($"oprom-data version -i \"{fdlg5}\"", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if Oprom-Code...\n");
                await RunProcessWithOutputAsync($"oprom-code version -i \"{fdlg5}\"", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Checking if FW-Data...\n");
                await RunProcessWithOutputAsync($"fw-data version -i \"{fdlg5}\"", file1, outputPath);

                AppendTextToRichTextBox(richTextBox1, "Finished checking file!");
            });
        }

        // Made this asyc so I can add more later.
        // Scan Hardware Button
        private async void button1_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            richTextBox1.Clear();

            // Stamp date
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dddd, MMMM dd yyyy hh:mm tt\n");
            AppendTextToRichTextBox(richTextBox1, $"Current date and time: " + formattedDateTime);
            AppendTextToRichTextBox(richTextBox1, $"ARC Firmware Tool Version: {currentVersion}{Environment.NewLine}");

            // Get GPU driver version
            string deviceToSearch = "Intel(R) Arc(TM)";

            await Task.Run(async () =>
            {
                // Read the resource files and copy them out.
                // Remember to set these as embedded resources or you will get mad for like 2 hours.
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

                String myProject = "ARC_Firmware_Tool";
                String file1 = "igsc.exe";
                String file2 = "igsc.dll";
                String file3 = "Intel-API.exe";
                String outputPath = System.IO.Path.GetTempPath();
                String executablePath = Path.Combine(outputPath, file1);

                // First file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file1))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file1, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // Next file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file2))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file2, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // Third file.
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file3))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file3, System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // AppendTextToRichTextBox(richTextBox1, "Looking up GPU driver version:\n");
                // Define and use the GetAndDisplayDriverVersions I want to combine this somehow? but I am trying to translate a powershell command and barely know what I'm doing.
                // Get-WmiObject Win32_PnPSignedDriver | Where-Object { $_.DeviceName -like '*Intel(R) Arc(TM)*' } | Select-Object -ExpandProperty DriverVersion
                string query = $"SELECT DeviceName, Manufacturer, DriverVersion FROM Win32_PnPSignedDriver WHERE DeviceName LIKE '%{deviceToSearch}%'";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject driver in searcher.Get())
                {
                    //string device = driver["DeviceName"].ToString();
                    //string manufacturer = driver["Manufacturer"].ToString();
                    string driverVersion = driver["DriverVersion"].ToString();
                    //string result = $"{device}\t{manufacturer}\t{driverVersion}\n";
                    string result = $"{driverVersion}\n";
                    AppendTextToRichTextBox(richTextBox1, $"Installed GPU driver version: " + result);
                }

                // Retrieve the GOP versions along with adapter names using Intel-API.exe
                List<(string AdapterName, string GopVersion)> adapterInfos = await GetGopVersionsAsync(outputPath);

                if (adapterInfos.Count == 0)
                {
                    AppendTextToRichTextBox(richTextBox1, "No adapters detected or unable to retrieve GOP versions.\n");
                }
                else
                {
                    foreach (var adapterInfo in adapterInfos)
                    {
                        string message = $"Adapter Name: {adapterInfo.AdapterName}, GOP (vBIOS) Version for Adapter: {adapterInfo.GopVersion}";
                        if (adapterInfo.GopVersion == "0.0.0")
                        {
                            message += " (GPU may not be initialized/active)";
                        }
                        message += "\n";

                        AppendTextToRichTextBox(richTextBox1, message);
                    }
                }

                // Call the rest using igsc
                AppendTextToRichTextBox(richTextBox1, "Listing Devices and FW/Oprom Versions:\n");
                await RunProcessWithOutputAsync($"list-devices -i", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Listing Devices HW Config:\n");
                await RunProcessWithOutputAsync($"fw hwconfig", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Listing FW Data and FW Code Versions:\n");
                await RunProcessWithOutputAsync($"fw-data version", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Listing OEM FW Version:\n");
                await RunProcessWithOutputAsync($"oem version", file1, outputPath);

                AppendTextToRichTextBox(richTextBox1, "Finished scanning hardware.");
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
                // Log or handle the error: File not found
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
                    // Log or handle the error: Process start failed
                    return adapterInfo;
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = await reader.ReadToEndAsync();
                    string[] lines = result.Split('\n');

                    string currentAdapterName = "";
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("*** Testing adapter #"))
                        {
                            currentAdapterName = "";
                        }
                        else if (line.Contains("Intel Adapter Name:"))
                        {
                            currentAdapterName = line.Split(':')[1].Trim();
                        }
                        else if (line.Contains("GOP Version :"))
                        {
                            string gopVersion = line.Split(':')[1].Trim();
                            if (!string.IsNullOrEmpty(currentAdapterName))
                            {
                                adapterInfo.Add((currentAdapterName, gopVersion));
                            }
                        }
                    }
                }
            }

            return adapterInfo;
        }


        // Try to re-factor smarter
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

            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            String myProject = "ARC_Firmware_Tool";
            String file1 = "igsc.exe";
            String file2 = "igsc.dll";
            String file3 = "Intel-API.exe";
            String outputPath = System.IO.Path.GetTempPath();

            // Create variables from other methods
            string fdlg1 = textBox1.Text;
            string fdlg2 = textBox2.Text;
            string fdlg3 = textBox3.Text;
            string fdlg4 = textBox4.Text;

            // First file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file1))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file1, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Next file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file2))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file2, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Third file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file3))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file3, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

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

            await RunProcessesAsync(file1, fdlg1, fdlg2, fdlg3, fdlg4, outputPath);

            AppendTextToRichTextBox(richTextBox1, "Flashing complete!");

            // Re-Enable buttons
            button3.Enabled = true;
            button2.Enabled = true;
        }

        private async Task RunProcessesAsync(string executableFileName, string fdlg1, string fdlg2, string fdlg3, string fdlg4, string outputPath)
        {
            // I should bring back the checkboxes but igsc has issues when I pass "null" values when a checkbox variable is empty.
            // Maybe I can do some kind of truncating so it doesnt show as a space but its easier to just auto force and auto allow downgrade.
            await Task.Run(async () =>
            {
                AppendTextToRichTextBox(richTextBox1, $"Flashing FW File:\n \"{fdlg1}\"\n");
                await RunProcessWithOutputAsync($"fw update -a -f -i \"{fdlg1}\"", executableFileName, outputPath);
                AppendTextToRichTextBox(richTextBox1, $"Flashing Oprom Data File:\n \"{fdlg2}\"\n");
                await RunProcessWithOutputAsync($"oprom-data update -a -i \"{fdlg2}\"", executableFileName, outputPath);
                AppendTextToRichTextBox(richTextBox1, $"Flashing Oprom Code File:\n \"{fdlg3}\"\n");
                await RunProcessWithOutputAsync($"oprom-code update -a -i \"{fdlg3}\"", executableFileName, outputPath);
                AppendTextToRichTextBox(richTextBox1, $"Flashing FW Data File:\n \"{fdlg4}\"\n");
                await RunProcessWithOutputAsync($"fw-data update -a -i \"{fdlg4}\"", executableFileName, outputPath);

            });
        }

        private async Task RunProcessWithOutputAsync(string arguments, string executableFileName, string outputPath)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = Path.Combine(outputPath, executableFileName);
                process.StartInfo.Arguments = arguments;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.EnableRaisingEvents = true;

                StringBuilder outputBuilder = new StringBuilder();

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await process.WaitForExitAsync();

                // Append the captured output to the RichTextBox
                AppendTextToRichTextBox(richTextBox1, outputBuilder.ToString() + Environment.NewLine);
            }
        }

        // Helper method for richTextBox1
        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() =>
                {
                    richTextBox.AppendText(text + Environment.NewLine);
                }));
            }
            else
            {
                richTextBox.AppendText(text + Environment.NewLine);
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

            // Tell them we are checking since this can take some time.
            AppendTextToRichTextBox(richTextBox1, "Checking for new Driver...");

            // Load the main URL
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = web.Load("https://www.intel.com/content/www/us/en/download/785597/intel-arc-iris-xe-graphics-windows.html");

            // Find the URL of the file you want to download we need to use the class names (inspect the page) to see what its serving us
            var downloadButton = document.DocumentNode.SelectSingleNode("//button[@class='dc-page-available-downloads-hero-button__cta dc-page-available-downloads-hero-button_cta available-download-button__cta']");
            string downloadUrl = downloadButton.GetAttributeValue("data-href", "");

            // Extract the file name from the second <span> element because sometimes download buttons contain multiple spans
            var fileNameElement = downloadButton.SelectNodes(".//span[@class='dc-page-available-downloads-hero-button_cta__label']");
            string fileName = fileNameElement[1].InnerText.Trim(); // Use [1] to select the second <span> element (it usually starts at 0)

            if (!string.IsNullOrEmpty(downloadUrl))
            {
                // Prompt the user for a save location
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Exe Files (*.exe)|*.exe|All Files (*.*)|*.*";
                saveFileDialog.FileName = fileName; // Use the extracted file name we pulled out of the<span>

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog.FileName;

                    // Use Task.Run to run the download operation on a separate thread (or we will lock the UI)
                    await Task.Run(() =>
                    {
                        using (WebClient client = new WebClient())
                        {
                            // Handle the DownloadProgressChanged event to update the download status
                            client.DownloadProgressChanged += (sender, args) =>
                            {
                                // Update the progress text in richTextBox1 with the progress percentage
                                int progressPercentage = args.ProgressPercentage;
                                string progressText = $"Downloading: {progressPercentage}%";

                                // Use BeginInvoke to update the UI control from the background thread
                                richTextBox1.BeginInvoke(new Action(() =>
                                {
                                    // Set the text to the latest progress text (so we don't append like 5 million lines to console)
                                    richTextBox1.Text = progressText;
                                }));
                            };

                            try
                            {
                                // Download the file async so we dont lock primary thread
                                client.DownloadFileAsync(new Uri(downloadUrl), savePath);

                                // Display message to indicate that the download has started
                                MessageBox.Show("Download started.");

                                // Handle the DownloadFileCompleted event to show "Download completed" message or else it will pop up after hitting ok even if the stream hasnt ended
                                client.DownloadFileCompleted += (sender, args) =>
                                {
                                    MessageBox.Show("Download completed.");
                                };
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error downloading file: " + ex.Message);
                            }
                        }
                    });
                }
            }
            else
            {
                MessageBox.Show("Could not parse download.");
            }

            // I literally can't believe I got this to work.
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
                        Title = "Save Update File"
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
                            Title = "Save Update File"
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
            saveFileDialog.FileName = $"ARC Flash log " + formattedDateTime;  // Set the default file name and extension
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

        // Trigger IGSC manually
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CopyIntelFilesToTemp();
                RunIGSCFromTemp();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Copy igsc.exe, igsc.dll and Intel-API.exe to temp
        static void CopyIntelFilesToTemp()
        {
            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            String myProject = "ARC_Firmware_Tool";
            String file1 = "igsc.exe";
            String file2 = "igsc.dll";
            String file3 = "Intel-API.exe";
            String outputPath = System.IO.Path.GetTempPath();
            String executablePath = Path.Combine(outputPath, file1);

            // First file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file1))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file1, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Next file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file2))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file2, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Third file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file3))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file3, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }
        }

        // Open a cmd window and run igsc.exe
        static void RunIGSCFromTemp()
        {
            string outputPath = System.IO.Path.GetTempPath();
            string executablePath = Path.Combine(outputPath, "igsc.exe");

            if (File.Exists(executablePath))
            {
                // Define what "startInfo" does
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    // Using /K instead of /C so the cmd window stays open after the command is executed
                    // Since IGSC is a console app this appears to create a new cmd window for the IGSC output on exit
                    Arguments = $"/K \"{executablePath}\" -v",
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false,
                    WorkingDirectory = Path.GetDirectoryName(executablePath)
                };

                // Execute "startInfo"
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
            }
            else
            {
                throw new Exception("Failed to copy igsc.exe or igsc.exe does not exist.");
            }
        }

        // API Debug
        // Trigger API manually
        private void aPIDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CopyIntelAPIFilesToTemp();
                RunAPIFromTemp();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Copy igsc.exe, igsc.dll and Intel-API.exe to temp
        static void CopyIntelAPIFilesToTemp()
        {
            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            String myProject = "ARC_Firmware_Tool";
            String file1 = "igsc.exe";
            String file2 = "igsc.dll";
            String file3 = "Intel-API.exe";
            String outputPath = System.IO.Path.GetTempPath();
            String executablePath = Path.Combine(outputPath, file3);

            // First file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file1))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file1, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Next file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file2))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file2, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            // Third file.
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(myProject + ".Resources." + file3))
            {
                using (System.IO.FileStream fileStream = new System.IO.FileStream(outputPath + "\\" + file3, System.IO.FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }
        }

        // Open a cmd window and run Intel-API.exe
        static void RunAPIFromTemp()
        {
            string outputPath = System.IO.Path.GetTempPath();
            string executablePath = Path.Combine(outputPath, "Intel-API.exe");

            if (File.Exists(executablePath))
            {
                // Define what "startInfo" does
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    // Using /K instead of /C so the cmd window stays open after the command is executed
                    // Since Intel-API is a console app this appears to create a new cmd window for the Intel-API output on exit
                    Arguments = $"/K \"{executablePath}\"",
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false,
                    WorkingDirectory = Path.GetDirectoryName(executablePath)
                };

                // Execute "startInfo"
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                }
            }
            else
            {
                throw new Exception("Failed to copy Intel-API.exe or Intel-API.exe does not exist.");
            }
        }

        // Leave main form close enabled but ask.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?\n\nThis is dangerous if you are flashing!", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        // Lets cleanup before we exit, I was trying to only do .bin and .rom containing "dg" in the name, but people can name them w/e they want.
        // I guess I could try and create a folder that puts everything we use in it. I will make the bad assumption that its temp and it should be safe. (Other things might use .bin and .rom here)
        // Method supporting the delete file operation for cleanups on exit.
        private void DeleteFileOrFilesByPattern(string directoryPath, string pattern)
        {
            if (pattern.StartsWith("."))
            {
                // Delete files by extension
                string[] filesToDelete = Directory.GetFiles(directoryPath, $"*{pattern}");
                foreach (string file in filesToDelete)
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Delete a specific file by name
                string filePath = Path.Combine(directoryPath, pattern);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        // Exit button
        private void button2_Click(object sender, EventArgs e)
        {
            string outputPath = Path.GetTempPath();

            // Define the file extensions and specific file names to delete
            string[] extensionsAndFilesToDelete = { ".bin", ".rom", "igsc.dll", "igsc.exe", "Intel-API.exe" }; // You can also add specific files here.

            // Delete files by extension and specific files
            foreach (string itemToDelete in extensionsAndFilesToDelete)
            {
                DeleteFileOrFilesByPattern(outputPath, itemToDelete);
            }

            System.Windows.Forms.Application.Exit();
        }

        // END System Block
    }
}
