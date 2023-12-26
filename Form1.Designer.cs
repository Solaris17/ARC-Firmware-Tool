using System.Reflection;

namespace ARC_Firmware_Tool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            richTextBox1 = new RichTextBox();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            toolTip1 = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            updateToolStripMenuItem = new ToolStripMenuItem();
            betaUpdateToolStripMenuItem = new ToolStripMenuItem();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveTextToolStripMenuItem = new ToolStripMenuItem();
            downloadLatestToolStripMenuItem = new ToolStripMenuItem();
            downloadDriverToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            manualToolStripMenuItem = new ToolStripMenuItem();
            textBox5 = new TextBox();
            button9 = new Button();
            button8 = new Button();
            stableToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 62);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(116, 32);
            label1.TabIndex = 0;
            label1.Text = "Firmware:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 139);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(164, 32);
            label2.TabIndex = 1;
            label2.Text = "Oprom (Data):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 311);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(189, 32);
            label3.TabIndex = 2;
            label3.Text = "FW Data/Config:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 228);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(171, 32);
            label5.TabIndex = 4;
            label5.Text = "Oprom (Code):";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.Location = new Point(449, 503);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(273, 68);
            button1.TabIndex = 5;
            button1.Text = "Scan HW";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(1012, 913);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(145, 64);
            button2.TabIndex = 6;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(15, 913);
            button3.Margin = new Padding(4);
            button3.Name = "button3";
            button3.Size = new Size(145, 64);
            button3.TabIndex = 7;
            button3.Text = "Flash";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Bottom;
            richTextBox1.Location = new Point(169, 580);
            richTextBox1.Margin = new Padding(4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(834, 397);
            richTextBox1.TabIndex = 8;
            richTextBox1.Text = "";
            // 
            // button4
            // 
            button4.Location = new Point(1012, 60);
            button4.Margin = new Padding(4);
            button4.Name = "button4";
            button4.Size = new Size(145, 47);
            button4.TabIndex = 13;
            button4.Text = "Browse";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(1012, 137);
            button5.Margin = new Padding(4);
            button5.Name = "button5";
            button5.Size = new Size(145, 51);
            button5.TabIndex = 14;
            button5.Text = "Browse";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(1012, 228);
            button6.Margin = new Padding(4);
            button6.Name = "button6";
            button6.Size = new Size(145, 49);
            button6.TabIndex = 15;
            button6.Text = "Browse";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(1012, 301);
            button7.Margin = new Padding(4);
            button7.Name = "button7";
            button7.Size = new Size(145, 53);
            button7.TabIndex = 16;
            button7.Text = "Browse";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(214, 58);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(699, 39);
            textBox1.TabIndex = 21;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(214, 139);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(699, 39);
            textBox2.TabIndex = 22;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(214, 228);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(699, 39);
            textBox3.TabIndex = 23;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(214, 305);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(699, 39);
            textBox4.TabIndex = 24;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, updateToolStripMenuItem, fileToolStripMenuItem, optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(1172, 40);
            menuStrip1.TabIndex = 25;
            menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.RightToLeft = RightToLeft.No;
            aboutToolStripMenuItem.Size = new Size(99, 36);
            aboutToolStripMenuItem.Text = "About";
            // 
            // updateToolStripMenuItem
            // 
            updateToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            updateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { stableToolStripMenuItem, betaUpdateToolStripMenuItem });
            updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            updateToolStripMenuItem.Size = new Size(111, 36);
            updateToolStripMenuItem.Text = "Update";
            // 
            // betaUpdateToolStripMenuItem
            // 
            betaUpdateToolStripMenuItem.Name = "betaUpdateToolStripMenuItem";
            betaUpdateToolStripMenuItem.Size = new Size(212, 44);
            betaUpdateToolStripMenuItem.Text = "Beta";
            betaUpdateToolStripMenuItem.Click += betaUpdateToolStripMenuItem_Click;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveTextToolStripMenuItem, downloadLatestToolStripMenuItem, downloadDriverToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 36);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveTextToolStripMenuItem
            // 
            saveTextToolStripMenuItem.Name = "saveTextToolStripMenuItem";
            saveTextToolStripMenuItem.Size = new Size(326, 44);
            saveTextToolStripMenuItem.Text = "Save Log";
            // 
            // downloadLatestToolStripMenuItem
            // 
            downloadLatestToolStripMenuItem.Name = "downloadLatestToolStripMenuItem";
            downloadLatestToolStripMenuItem.Size = new Size(326, 44);
            downloadLatestToolStripMenuItem.Text = "Download vBios";
            // 
            // downloadDriverToolStripMenuItem
            // 
            downloadDriverToolStripMenuItem.Name = "downloadDriverToolStripMenuItem";
            downloadDriverToolStripMenuItem.Size = new Size(326, 44);
            downloadDriverToolStripMenuItem.Text = "Download Driver";
            downloadDriverToolStripMenuItem.Click += downloadDriverToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manualToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(118, 36);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // manualToolStripMenuItem
            // 
            manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            manualToolStripMenuItem.Size = new Size(359, 44);
            manualToolStripMenuItem.Text = "Manual Mode";
            manualToolStripMenuItem.Click += manualToolStripMenuItem_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(214, 375);
            textBox5.Margin = new Padding(4);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(699, 39);
            textBox5.TabIndex = 28;
            // 
            // button9
            // 
            button9.Location = new Point(15, 375);
            button9.Margin = new Padding(4);
            button9.Name = "button9";
            button9.Size = new Size(171, 49);
            button9.TabIndex = 27;
            button9.Text = "Check a file:";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(1012, 375);
            button8.Margin = new Padding(4);
            button8.Name = "button8";
            button8.Size = new Size(145, 49);
            button8.TabIndex = 29;
            button8.Text = "Browse";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // stableToolStripMenuItem
            // 
            stableToolStripMenuItem.Name = "stableToolStripMenuItem";
            stableToolStripMenuItem.Size = new Size(212, 44);
            stableToolStripMenuItem.Text = "Stable";
            stableToolStripMenuItem.Click += stableToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1172, 994);
            Controls.Add(button8);
            Controls.Add(textBox5);
            Controls.Add(button9);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ARC Firmware Tool (BETA)";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Button button1;
        private Button button2;
        private Button button3;
        private RichTextBox richTextBox1;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private ToolTip toolTip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveTextToolStripMenuItem;
        private ToolStripMenuItem downloadLatestToolStripMenuItem;
        private ToolStripMenuItem downloadDriverToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem manualToolStripMenuItem;
        private ToolStripMenuItem betaUpdateToolStripMenuItem;
        private ToolStripMenuItem stableToolStripMenuItem;
    }
}