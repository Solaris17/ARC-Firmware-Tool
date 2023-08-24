using System.Diagnostics;
using System.Text;
using Octokit;


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
        // Specify the current version (that you will release) so that it will always pull the newer one (latest tag)
        //private string currentVersion = "0.9.0";
        private string currentVersion = "1.7.0";

        public Form1()
        {
            InitializeComponent();

            // About box event handler
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            // Connect the saveTextToolStripMenuItem_Click event handler
            saveTextToolStripMenuItem.Click += saveTextToolStripMenuItem_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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

            await Task.Run(async () =>
            {
                // Read the resource files and copy them out.
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

                String myProject = "ARC_Firmware_Tool";
                String file1 = "igsc.exe";
                String file2 = "igsc.dll";
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

                AppendTextToRichTextBox(richTextBox1, "Listing Devices and FW/Oprom Versions:\n");
                await RunProcessWithOutputAsync($"list-devices -i", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Listing Devices HW Config:\n");
                await RunProcessWithOutputAsync($"fw hwconfig", file1, outputPath);
                AppendTextToRichTextBox(richTextBox1, "Listing FW Data and FW Code Versions:\n");
                await RunProcessWithOutputAsync($"fw-data version", file1, outputPath);

                AppendTextToRichTextBox(richTextBox1, "Finished scanning hardware.");
            });
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
            AppendTextToRichTextBox(richTextBox1, "Now Flashing...\nDo not close program while flashing is in progress!\n");

            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            String myProject = "ARC_Firmware_Tool";
            String file1 = "igsc.exe";
            String file2 = "igsc.dll";
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

        // About box
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        // Lets do a software update
        // Do some version checks
        // Maybe make a temp process to close and open the new version later but file handling is hard and I hate it
        private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool updateNeeded = await CheckAndUpdateVersionAsync();

                if (updateNeeded)
                {
                    var saveFileDialog = new SaveFileDialog
                    {
                        FileName = "ARC Firmware Tool.exe",
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
                    MessageBox.Show("Your already up to date!", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> CheckAndUpdateVersionAsync()
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

        private async Task DownloadAndSaveUpdate(string savePath)
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
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

        // Exit button
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
