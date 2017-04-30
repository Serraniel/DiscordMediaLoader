namespace DML.Application
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlSettings = new System.Windows.Forms.GroupBox();
            this.lbThreadLimit = new System.Windows.Forms.Label();
            this.edThreadLimit = new System.Windows.Forms.NumericUpDown();
            this.cbSkipExisting = new System.Windows.Forms.CheckBox();
            this.edNameScheme = new System.Windows.Forms.TextBox();
            this.lbNameScheme = new System.Windows.Forms.Label();
            this.btnSearchFolders = new System.Windows.Forms.Button();
            this.edOperatingFolder = new System.Windows.Forms.TextBox();
            this.lbOperatingFolder = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddJob = new System.Windows.Forms.Button();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.lbChannel = new System.Windows.Forms.Label();
            this.cbGuild = new System.Windows.Forms.ComboBox();
            this.lbGuild = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lbProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lbxJobs = new System.Windows.Forms.ListBox();
            this.tmrRefreshProgress = new System.Windows.Forms.Timer(this.components);
            this.lblVersionPlaceholder = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDropDown = new System.Windows.Forms.ToolStripSplitButton();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visitGithubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edThreadLimit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.lbThreadLimit);
            this.pnlSettings.Controls.Add(this.edThreadLimit);
            this.pnlSettings.Controls.Add(this.cbSkipExisting);
            this.pnlSettings.Controls.Add(this.edNameScheme);
            this.pnlSettings.Controls.Add(this.lbNameScheme);
            this.pnlSettings.Controls.Add(this.btnSearchFolders);
            this.pnlSettings.Controls.Add(this.edOperatingFolder);
            this.pnlSettings.Controls.Add(this.lbOperatingFolder);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(553, 93);
            this.pnlSettings.TabIndex = 0;
            this.pnlSettings.TabStop = false;
            this.pnlSettings.Text = "Settings";
            // 
            // lbThreadLimit
            // 
            this.lbThreadLimit.AutoSize = true;
            this.lbThreadLimit.Location = new System.Drawing.Point(12, 67);
            this.lbThreadLimit.Name = "lbThreadLimit";
            this.lbThreadLimit.Size = new System.Drawing.Size(64, 13);
            this.lbThreadLimit.TabIndex = 6;
            this.lbThreadLimit.Text = "Thread limit:";
            // 
            // edThreadLimit
            // 
            this.edThreadLimit.Location = new System.Drawing.Point(113, 65);
            this.edThreadLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edThreadLimit.Name = "edThreadLimit";
            this.edThreadLimit.Size = new System.Drawing.Size(120, 20);
            this.edThreadLimit.TabIndex = 7;
            this.edThreadLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edThreadLimit.ValueChanged += new System.EventHandler(this.DoSomethingChanged);
            // 
            // cbSkipExisting
            // 
            this.cbSkipExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkipExisting.AutoSize = true;
            this.cbSkipExisting.Location = new System.Drawing.Point(447, 42);
            this.cbSkipExisting.Name = "cbSkipExisting";
            this.cbSkipExisting.Size = new System.Drawing.Size(106, 17);
            this.cbSkipExisting.TabIndex = 5;
            this.cbSkipExisting.Text = "Skip existing files";
            this.cbSkipExisting.UseVisualStyleBackColor = true;
            this.cbSkipExisting.TextChanged += new System.EventHandler(this.DoSomethingChanged);
            // 
            // edNameScheme
            // 
            this.edNameScheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edNameScheme.Location = new System.Drawing.Point(113, 39);
            this.edNameScheme.Name = "edNameScheme";
            this.edNameScheme.Size = new System.Drawing.Size(328, 20);
            this.edNameScheme.TabIndex = 4;
            this.edNameScheme.TextChanged += new System.EventHandler(this.DoSomethingChanged);
            // 
            // lbNameScheme
            // 
            this.lbNameScheme.AutoSize = true;
            this.lbNameScheme.Location = new System.Drawing.Point(12, 42);
            this.lbNameScheme.Name = "lbNameScheme";
            this.lbNameScheme.Size = new System.Drawing.Size(95, 13);
            this.lbNameScheme.TabIndex = 3;
            this.lbNameScheme.Text = "File name scheme:";
            // 
            // btnSearchFolders
            // 
            this.btnSearchFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchFolders.Location = new System.Drawing.Point(522, 11);
            this.btnSearchFolders.Name = "btnSearchFolders";
            this.btnSearchFolders.Size = new System.Drawing.Size(25, 23);
            this.btnSearchFolders.TabIndex = 2;
            this.btnSearchFolders.Text = "...";
            this.btnSearchFolders.UseVisualStyleBackColor = true;
            this.btnSearchFolders.Click += new System.EventHandler(this.btnSearchFolders_Click);
            // 
            // edOperatingFolder
            // 
            this.edOperatingFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edOperatingFolder.Location = new System.Drawing.Point(113, 13);
            this.edOperatingFolder.Name = "edOperatingFolder";
            this.edOperatingFolder.Size = new System.Drawing.Size(403, 20);
            this.edOperatingFolder.TabIndex = 1;
            this.edOperatingFolder.TextChanged += new System.EventHandler(this.DoSomethingChanged);
            // 
            // lbOperatingFolder
            // 
            this.lbOperatingFolder.AutoSize = true;
            this.lbOperatingFolder.Location = new System.Drawing.Point(12, 16);
            this.lbOperatingFolder.Name = "lbOperatingFolder";
            this.lbOperatingFolder.Size = new System.Drawing.Size(85, 13);
            this.lbOperatingFolder.TabIndex = 0;
            this.lbOperatingFolder.Text = "Operating folder:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddJob);
            this.groupBox1.Controls.Add(this.cbChannel);
            this.groupBox1.Controls.Add(this.lbChannel);
            this.groupBox1.Controls.Add(this.cbGuild);
            this.groupBox1.Controls.Add(this.lbGuild);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add a job";
            // 
            // btnAddJob
            // 
            this.btnAddJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddJob.Location = new System.Drawing.Point(481, 19);
            this.btnAddJob.Name = "btnAddJob";
            this.btnAddJob.Size = new System.Drawing.Size(66, 23);
            this.btnAddJob.TabIndex = 4;
            this.btnAddJob.Text = "&Add";
            this.btnAddJob.UseVisualStyleBackColor = true;
            this.btnAddJob.Click += new System.EventHandler(this.btnAddJob_Click);
            // 
            // cbChannel
            // 
            this.cbChannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbChannel.FormattingEnabled = true;
            this.cbChannel.Location = new System.Drawing.Point(294, 19);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(181, 21);
            this.cbChannel.TabIndex = 3;
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.Location = new System.Drawing.Point(239, 22);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(49, 13);
            this.lbChannel.TabIndex = 2;
            this.lbChannel.Text = "Channel:";
            // 
            // cbGuild
            // 
            this.cbGuild.FormattingEnabled = true;
            this.cbGuild.Location = new System.Drawing.Point(52, 19);
            this.cbGuild.Name = "cbGuild";
            this.cbGuild.Size = new System.Drawing.Size(181, 21);
            this.cbGuild.TabIndex = 1;
            this.cbGuild.SelectedIndexChanged += new System.EventHandler(this.cbGuild_SelectedIndexChanged);
            // 
            // lbGuild
            // 
            this.lbGuild.AutoSize = true;
            this.lbGuild.Location = new System.Drawing.Point(12, 22);
            this.lbGuild.Name = "lbGuild";
            this.lbGuild.Size = new System.Drawing.Size(34, 13);
            this.lbGuild.TabIndex = 0;
            this.lbGuild.Text = "Guild:";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbProgress,
            this.lbProgress,
            this.lblVersionPlaceholder,
            this.lbVersion,
            this.btnDropDown});
            this.statusStrip.Location = new System.Drawing.Point(0, 311);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(553, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // pgbProgress
            // 
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // lbProgress
            // 
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.lbxJobs);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 161);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jobs";
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(3, 135);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(547, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lbxJobs
            // 
            this.lbxJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxJobs.FormattingEnabled = true;
            this.lbxJobs.Location = new System.Drawing.Point(6, 19);
            this.lbxJobs.Name = "lbxJobs";
            this.lbxJobs.Size = new System.Drawing.Size(541, 108);
            this.lbxJobs.TabIndex = 0;
            // 
            // tmrRefreshProgress
            // 
            this.tmrRefreshProgress.Enabled = true;
            this.tmrRefreshProgress.Interval = 500;
            this.tmrRefreshProgress.Tick += new System.EventHandler(this.tmrRefreshProgress_Tick);
            // 
            // lblVersionPlaceholder
            // 
            this.lblVersionPlaceholder.Name = "lblVersionPlaceholder";
            this.lblVersionPlaceholder.Size = new System.Drawing.Size(271, 17);
            this.lblVersionPlaceholder.Spring = true;
            // 
            // lbVersion
            // 
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(118, 17);
            this.lbVersion.Text = "v https://github.com";
            // 
            // btnDropDown
            // 
            this.btnDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visitGithubToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.btnDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDropDown.Name = "btnDropDown";
            this.btnDropDown.Size = new System.Drawing.Size(16, 20);
            this.btnDropDown.Text = "Options";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // visitGithubToolStripMenuItem
            // 
            this.visitGithubToolStripMenuItem.Name = "visitGithubToolStripMenuItem";
            this.visitGithubToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.visitGithubToolStripMenuItem.Text = "Visit Github";
            this.visitGithubToolStripMenuItem.Click += new System.EventHandler(this.visitGithubToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 333);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(100, 75);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discord Medial Loader";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edThreadLimit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pnlSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbThreadLimit;
        private System.Windows.Forms.NumericUpDown edThreadLimit;
        private System.Windows.Forms.CheckBox cbSkipExisting;
        private System.Windows.Forms.TextBox edNameScheme;
        private System.Windows.Forms.Label lbNameScheme;
        private System.Windows.Forms.Button btnSearchFolders;
        private System.Windows.Forms.TextBox edOperatingFolder;
        private System.Windows.Forms.Label lbOperatingFolder;
        private System.Windows.Forms.Button btnAddJob;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.ComboBox cbGuild;
        private System.Windows.Forms.Label lbGuild;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListBox lbxJobs;
        private System.Windows.Forms.ToolStripProgressBar pgbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lbProgress;
        private System.Windows.Forms.Timer tmrRefreshProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblVersionPlaceholder;
        private System.Windows.Forms.ToolStripStatusLabel lbVersion;
        private System.Windows.Forms.ToolStripSplitButton btnDropDown;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visitGithubToolStripMenuItem;
    }
}