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
            this.lbGuild = new System.Windows.Forms.Label();
            this.cbGuilds = new System.Windows.Forms.ComboBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.cbChannels = new System.Windows.Forms.ComboBox();
            this.lbChannel = new System.Windows.Forms.Label();
            this.cbLimitDate = new System.Windows.Forms.CheckBox();
            this.dtpLimit = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbPath = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // cbGuilds
            // 
            this.cbGuilds.FormattingEnabled = true;
            this.cbGuilds.Location = new System.Drawing.Point(52, 32);
            this.cbGuilds.Name = "cbGuilds";
            this.cbGuilds.Size = new System.Drawing.Size(164, 21);
            this.cbGuilds.TabIndex = 1;
            this.cbGuilds.SelectedIndexChanged += new System.EventHandler(this.cbGuilds_SelectedIndexChanged);
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
            // cbChannels
            // 
            this.cbChannels.FormattingEnabled = true;
            this.cbChannels.Location = new System.Drawing.Point(264, 32);
            this.cbChannels.Name = "cbChannels";
            this.cbChannels.Size = new System.Drawing.Size(164, 21);
            this.cbChannels.TabIndex = 4;
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
            // cbLimitDate
            // 
            this.cbLimitDate.AutoSize = true;
            this.cbLimitDate.Location = new System.Drawing.Point(15, 73);
            this.cbLimitDate.Name = "cbLimitDate";
            this.cbLimitDate.Size = new System.Drawing.Size(137, 17);
            this.cbLimitDate.TabIndex = 5;
            this.cbLimitDate.Text = "Only media posted after";
            this.cbLimitDate.UseVisualStyleBackColor = true;
            // 
            // dtpLimit
            // 
            this.dtpLimit.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLimit.Location = new System.Drawing.Point(158, 70);
            this.dtpLimit.Name = "dtpLimit";
            this.dtpLimit.Size = new System.Drawing.Size(95, 20);
            this.dtpLimit.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(403, 105);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = true;
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(342, 20);
            this.textBox1.TabIndex = 7;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 142);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(416, 23);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Text = "Start downloading";
            this.btnDownload.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 177);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.textBox1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbGuild;
        private System.Windows.Forms.ComboBox cbGuilds;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.ComboBox cbChannels;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.CheckBox cbLimitDate;
        private System.Windows.Forms.DateTimePicker dtpLimit;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDownload;
    }
}