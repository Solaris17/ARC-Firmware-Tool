using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

// I am so trash at C# please help

namespace ARC_Firmware_Tool
{
    public partial class Form1 : Form
    {

        private object syncGate = new object();
        private Process process;
        private StringBuilder output = new StringBuilder();
        private bool outputChanged;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Buttons here

        // Firmware Browse button
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

        // Scan Hardware Button
        public void button1_Click(object sender, EventArgs e)
        {
            lock (syncGate)
            {
                if (process != null) return;
            }

            output.Clear();
            outputChanged = false;
            richTextBox1.Text = "";

            // Read the resource files and copy them out.
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            String myProject = "ARC_Firmware_Tool";
            String file1 = "igsc.exe";
            String file2 = "igsc.dll";
            String outputPath = System.IO.Path.GetTempPath();

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

            process = new Process();
            process.StartInfo.FileName = System.IO.Path.GetTempPath() + file1;
            process.StartInfo.Arguments = "list-devices --info";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.EnableRaisingEvents = true;
            process.Start();

            new Thread(ReadData1) { IsBackground = true }.Start();
        }

        private void ReadData1()
        {
            var input = process.StandardOutput;
            int nextChar;
            while ((nextChar = input.Read()) >= 0)
            {
                lock (syncGate)
                {
                    output.Append((char)nextChar);
                    if (!outputChanged)
                    {
                        outputChanged = true;
                        BeginInvoke(new Action(OnOutputChanged));
                    }
                }
            }
            lock (syncGate)
            {
                process.Dispose();
                process = null;
            }
        }

        private void OnOutputChanged()
        {
            lock (syncGate)
            {
                richTextBox1.Text = output.ToString();
                outputChanged = false;
            }
        }

        // Flash Button
        private async void button3_Click(object sender, EventArgs e)
        {
            // Begin flash process

            // Clear the RichTextBox
            richTextBox1.Clear();

            // Display the introductory message before the process output
            AppendTextToRichTextBox(richTextBox1, "Now Flashing...\nDo not close program while flashing is in progress!\n\n");

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

            await RunProcessesAsync(file1, fdlg1, fdlg2, fdlg3, fdlg4);
        }

        private async Task RunProcessesAsync(string executableFileName, string fdlg1, string fdlg2, string fdlg3, string fdlg4)
        {
            await Task.Run(async () =>
            {
                await RunProcessWithOutputAsync($"fw update -a -f -i \"{fdlg1}\"", executableFileName);
                await RunProcessWithOutputAsync($"oprom-data update -a -i \"{fdlg2}\"", executableFileName);
                await RunProcessWithOutputAsync($"oprom-code update -a -i \"{fdlg3}\"", executableFileName);
                await RunProcessWithOutputAsync($"fw-data update -a -i \"{fdlg4}\"", executableFileName);
            });
        }

        private async Task RunProcessWithOutputAsync(string arguments, string executableFileName)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = System.IO.Path.GetTempPath() + executableFileName;
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

        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new Action(() =>
                {
                    richTextBox.AppendText(text);
                }));
            }
            else
            {
                richTextBox.AppendText(text);
            }
        }


        // Exit button
        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        // Exit the about box
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }
    }
}
