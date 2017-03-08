namespace Discord_Media_Loader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbPath = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbxPath = new System.Windows.Forms.TextBox();
            this.dtpLimit = new System.Windows.Forms.DateTimePicker();
            this.btnDownload = new System.Windows.Forms.Button();
            this.cbLimitDate = new System.Windows.Forms.CheckBox();
            this.lbThread = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            this.nupThreadCount = new System.Windows.Forms.NumericUpDown();
            this.lbChannel = new System.Windows.Forms.Label();
            this.lbScanCount = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbDownload = new System.Windows.Forms.Label();
            this.cbGuilds = new System.Windows.Forms.ComboBox();
            this.lbGuild = new System.Windows.Forms.Label();
            this.cbSkip = new System.Windows.Forms.CheckBox();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbGithub = new System.Windows.Forms.LinkLabel();
            this.lbAbout = new System.Windows.Forms.LinkLabel();
            this.lbVersion = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nupThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(12, 110);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(32, 13);
            this.lbPath.TabIndex = 8;
            this.lbPath.Text = "Path:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(403, 105);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbxPath
            // 
            this.tbxPath.Location = new System.Drawing.Point(50, 107);
            this.tbxPath.Name = "tbxPath";
            this.tbxPath.Size = new System.Drawing.Size(342, 20);
            this.tbxPath.TabIndex = 7;
            // 
            // dtpLimit
            // 
            this.dtpLimit.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLimit.Location = new System.Drawing.Point(158, 70);
            this.dtpLimit.Name = "dtpLimit";
            this.dtpLimit.Size = new System.Drawing.Size(95, 20);
            this.dtpLimit.TabIndex = 6;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 167);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(415, 23);
            this.btnDownload.TabIndex = 20;
            this.btnDownload.Text = "Start downloading";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // cbLimitDate
            // 
            this.cbLimitDate.AutoSize = true;
            this.cbLimitDate.Checked = true;
            this.cbLimitDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLimitDate.Location = new System.Drawing.Point(15, 73);
            this.cbLimitDate.Name = "cbLimitDate";
            this.cbLimitDate.Size = new System.Drawing.Size(137, 17);
            this.cbLimitDate.TabIndex = 5;
            this.cbLimitDate.Text = "Only media posted after";
            this.cbLimitDate.UseVisualStyleBackColor = true;
            this.cbLimitDate.CheckedChanged += new System.EventHandler(this.cbLimitDate_CheckedChanged);
            // 
            // lbThread
            // 
            this.lbThread.AutoSize = true;
            this.lbThread.Location = new System.Drawing.Point(12, 141);
            this.lbThread.Name = "lbThread";
            this.lbThread.Size = new System.Drawing.Size(64, 13);
            this.lbThread.TabIndex = 11;
            this.lbThread.Text = "Thread limit:";
            // 
            // cbChannels
            // 
            this.cbChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Location = new System.Drawing.Point(264, 32);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(164, 21);
            this.cbChannels.TabIndex = 4;
            // 
            // nupThreadCount
            // 
            this.nupThreadCount.Location = new System.Drawing.Point(82, 139);
            this.nupThreadCount.Name = "nupThreadCount";
            this.nupThreadCount.Size = new System.Drawing.Size(70, 20);
            this.nupThreadCount.TabIndex = 12;
            this.nupThreadCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.Location = new System.Drawing.Point(224, 38);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(34, 13);
            this.lbChannel.TabIndex = 3;
            this.lbChannel.Text = "Guild:";
            // 
            // lbScanCount
            // 
            this.lbScanCount.AutoSize = true;
            this.lbScanCount.Location = new System.Drawing.Point(12, 203);
            this.lbScanCount.Name = "lbScanCount";
            this.lbScanCount.Size = new System.Drawing.Size(102, 13);
            this.lbScanCount.TabIndex = 13;
            this.lbScanCount.Text = "Messages scanned:";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(12, 9);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(58, 13);
            this.lbUsername.TabIndex = 2;
            this.lbUsername.Text = "Username:";
            // 
            // lbDownload
            // 
            this.lbDownload.AutoSize = true;
            this.lbDownload.Location = new System.Drawing.Point(234, 203);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(92, 13);
            this.lbDownload.TabIndex = 14;
            this.lbDownload.Text = "Files downloaded:";
            // 
            // cbGuilds
            // 
            this.cbGuilds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGuilds.FormattingEnabled = true;
            this.cbGuilds.Location = new System.Drawing.Point(52, 32);
            this.cbGuilds.Name = "cbGuilds";
            this.cbGuilds.Size = new System.Drawing.Size(164, 21);
            this.cbGuilds.TabIndex = 1;
            this.cbGuilds.SelectedIndexChanged += new System.EventHandler(this.cbGuilds_SelectedIndexChanged);
            // 
            // lbGuild
            // 
            this.lbGuild.AutoSize = true;
            this.lbGuild.Location = new System.Drawing.Point(12, 35);
            this.lbGuild.Name = "lbGuild";
            this.lbGuild.Size = new System.Drawing.Size(34, 13);
            this.lbGuild.TabIndex = 0;
            this.lbGuild.Text = "Guild:";
            // 
            // cbSkip
            // 
            this.cbSkip.AutoSize = true;
            this.cbSkip.Checked = true;
            this.cbSkip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkip.Location = new System.Drawing.Point(227, 141);
            this.cbSkip.Name = "cbSkip";
            this.cbSkip.Size = new System.Drawing.Size(106, 17);
            this.cbSkip.TabIndex = 16;
            this.cbSkip.Text = "Skip existing files";
            this.cbSkip.UseVisualStyleBackColor = true;
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Location = new System.Drawing.Point(12, 274);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(151, 13);
            this.lbCopyright.TabIndex = 22;
            this.lbCopyright.Text = "Copyright (c) 2017 by Serraniel";
            // 
            // lbGithub
            // 
            this.lbGithub.AutoSize = true;
            this.lbGithub.Location = new System.Drawing.Point(169, 274);
            this.lbGithub.Name = "lbGithub";
            this.lbGithub.Size = new System.Drawing.Size(40, 13);
            this.lbGithub.TabIndex = 23;
            this.lbGithub.TabStop = true;
            this.lbGithub.Text = "GitHub";
            this.lbGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbGithub_LinkClicked);
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            this.lbAbout.Location = new System.Drawing.Point(223, 274);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(35, 13);
            this.lbAbout.TabIndex = 24;
            this.lbAbout.TabStop = true;
            this.lbAbout.Text = "About";
            this.lbAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbAbout_LinkClicked);
            // 
            // lbVersion
            // 
            this.lbVersion.Location = new System.Drawing.Point(328, 274);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(100, 15);
            this.lbVersion.TabIndex = 26;
            this.lbVersion.TabStop = true;
            this.lbVersion.Text = "version";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbVersion_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 296);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbAbout);
            this.Controls.Add(this.lbGithub);
            this.Controls.Add(this.lbCopyright);
            this.Controls.Add(this.cbSkip);
            this.Controls.Add(this.lbDownload);
            this.Controls.Add(this.lbScanCount);
            this.Controls.Add(this.nupThreadCount);
            this.Controls.Add(this.lbThread);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.tbxPath);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpLimit);
            this.Controls.Add(this.cbLimitDate);
            this.Controls.Add(this.cbChannels);
            this.Controls.Add(this.lbChannel);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.cbGuilds);
            this.Controls.Add(this.lbGuild);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discord Media Loader";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nupThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbxPath;
        private System.Windows.Forms.DateTimePicker dtpLimit;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckBox cbLimitDate;
        private System.Windows.Forms.Label lbThread;
        private System.Windows.Forms.ComboBox cbChannels;
        private System.Windows.Forms.NumericUpDown nupThreadCount;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.Label lbScanCount;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbDownload;
        private System.Windows.Forms.ComboBox cbGuilds;
        private System.Windows.Forms.Label lbGuild;
        private System.Windows.Forms.CheckBox cbSkip;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.LinkLabel lbGithub;
        private System.Windows.Forms.LinkLabel lbAbout;
        private System.Windows.Forms.LinkLabel lbVersion;
    }
}