namespace DML.Application.Dialogs
{
    partial class LoginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.tbcLoginMethods = new System.Windows.Forms.TabControl();
            this.tpgToken = new System.Windows.Forms.TabPage();
            this.tpgUserdata = new System.Windows.Forms.TabPage();
            this.lbToken = new System.Windows.Forms.Label();
            this.edToken = new System.Windows.Forms.TextBox();
            this.lbHowToToken = new System.Windows.Forms.Label();
            this.edEmail = new System.Windows.Forms.TextBox();
            this.edPassword = new System.Windows.Forms.TextBox();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.cbUseUserdata = new System.Windows.Forms.CheckBox();
            this.cbSavePassword = new System.Windows.Forms.CheckBox();
            this.lbUserdataHints = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            this.tbcLoginMethods.SuspendLayout();
            this.tpgToken.SuspendLayout();
            this.tpgUserdata.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlButtons.Controls.Add(this.btnAbort);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 168);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(426, 51);
            this.pnlButtons.TabIndex = 0;
            // 
            // tbcLoginMethods
            // 
            this.tbcLoginMethods.Controls.Add(this.tpgToken);
            this.tbcLoginMethods.Controls.Add(this.tpgUserdata);
            this.tbcLoginMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcLoginMethods.Location = new System.Drawing.Point(0, 0);
            this.tbcLoginMethods.Name = "tbcLoginMethods";
            this.tbcLoginMethods.SelectedIndex = 0;
            this.tbcLoginMethods.Size = new System.Drawing.Size(426, 168);
            this.tbcLoginMethods.TabIndex = 1;
            // 
            // tpgToken
            // 
            this.tpgToken.Controls.Add(this.lbHowToToken);
            this.tpgToken.Controls.Add(this.edToken);
            this.tpgToken.Controls.Add(this.lbToken);
            this.tpgToken.Location = new System.Drawing.Point(4, 22);
            this.tpgToken.Name = "tpgToken";
            this.tpgToken.Padding = new System.Windows.Forms.Padding(3);
            this.tpgToken.Size = new System.Drawing.Size(418, 142);
            this.tpgToken.TabIndex = 0;
            this.tpgToken.Text = "Token";
            this.tpgToken.UseVisualStyleBackColor = true;
            // 
            // tpgUserdata
            // 
            this.tpgUserdata.Controls.Add(this.lbUserdataHints);
            this.tpgUserdata.Controls.Add(this.cbSavePassword);
            this.tpgUserdata.Controls.Add(this.cbUseUserdata);
            this.tpgUserdata.Controls.Add(this.lbPassword);
            this.tpgUserdata.Controls.Add(this.lbEmail);
            this.tpgUserdata.Controls.Add(this.edPassword);
            this.tpgUserdata.Controls.Add(this.edEmail);
            this.tpgUserdata.Location = new System.Drawing.Point(4, 22);
            this.tpgUserdata.Name = "tpgUserdata";
            this.tpgUserdata.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUserdata.Size = new System.Drawing.Size(418, 142);
            this.tpgUserdata.TabIndex = 1;
            this.tpgUserdata.Text = "Userdata";
            this.tpgUserdata.UseVisualStyleBackColor = true;
            // 
            // lbToken
            // 
            this.lbToken.AutoSize = true;
            this.lbToken.Location = new System.Drawing.Point(3, 9);
            this.lbToken.Name = "lbToken";
            this.lbToken.Size = new System.Drawing.Size(66, 13);
            this.lbToken.TabIndex = 0;
            this.lbToken.Text = "Login token:";
            // 
            // edToken
            // 
            this.edToken.Location = new System.Drawing.Point(75, 6);
            this.edToken.Name = "edToken";
            this.edToken.Size = new System.Drawing.Size(335, 20);
            this.edToken.TabIndex = 1;
            // 
            // lbHowToToken
            // 
            this.lbHowToToken.Location = new System.Drawing.Point(3, 52);
            this.lbHowToToken.Name = "lbHowToToken";
            this.lbHowToToken.Size = new System.Drawing.Size(412, 87);
            this.lbHowToToken.TabIndex = 2;
            this.lbHowToToken.Text = resources.GetString("lbHowToToken.Text");
            // 
            // edEmail
            // 
            this.edEmail.Location = new System.Drawing.Point(47, 6);
            this.edEmail.Name = "edEmail";
            this.edEmail.Size = new System.Drawing.Size(133, 20);
            this.edEmail.TabIndex = 0;
            // 
            // edPassword
            // 
            this.edPassword.Location = new System.Drawing.Point(279, 6);
            this.edPassword.Name = "edPassword";
            this.edPassword.PasswordChar = '•';
            this.edPassword.Size = new System.Drawing.Size(133, 20);
            this.edPassword.TabIndex = 1;
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Location = new System.Drawing.Point(6, 9);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(35, 13);
            this.lbEmail.TabIndex = 2;
            this.lbEmail.Text = "Email:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(217, 9);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(56, 13);
            this.lbPassword.TabIndex = 3;
            this.lbPassword.Text = "Password:";
            // 
            // cbUseUserdata
            // 
            this.cbUseUserdata.AutoSize = true;
            this.cbUseUserdata.Location = new System.Drawing.Point(6, 32);
            this.cbUseUserdata.Name = "cbUseUserdata";
            this.cbUseUserdata.Size = new System.Drawing.Size(139, 17);
            this.cbUseUserdata.TabIndex = 4;
            this.cbUseUserdata.Text = "Use login with user data";
            this.cbUseUserdata.UseVisualStyleBackColor = true;
            // 
            // cbSavePassword
            // 
            this.cbSavePassword.AutoSize = true;
            this.cbSavePassword.Location = new System.Drawing.Point(313, 32);
            this.cbSavePassword.Name = "cbSavePassword";
            this.cbSavePassword.Size = new System.Drawing.Size(99, 17);
            this.cbSavePassword.TabIndex = 5;
            this.cbSavePassword.Text = "Save password";
            this.cbSavePassword.UseVisualStyleBackColor = true;
            // 
            // lbUserdataHints
            // 
            this.lbUserdataHints.Location = new System.Drawing.Point(3, 52);
            this.lbUserdataHints.Name = "lbUserdataHints";
            this.lbUserdataHints.Size = new System.Drawing.Size(412, 87);
            this.lbUserdataHints.TabIndex = 6;
            this.lbUserdataHints.Text = "Login with email and password is not recommended. If you use two factor authentic" +
    "ation this can cause a ban of your discord account.\r\n\r\nFor safety reasons we rec" +
    "ommend to login with login token.";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(267, 16);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(348, 16);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 1;
            this.btnAbort.Text = "&Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // LoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 219);
            this.Controls.Add(this.tbcLoginMethods);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginDialog_FormClosing);
            this.Shown += new System.EventHandler(this.LoginDialog_Shown);
            this.pnlButtons.ResumeLayout(false);
            this.tbcLoginMethods.ResumeLayout(false);
            this.tpgToken.ResumeLayout(false);
            this.tpgToken.PerformLayout();
            this.tpgUserdata.ResumeLayout(false);
            this.tpgUserdata.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.TabControl tbcLoginMethods;
        private System.Windows.Forms.TabPage tpgToken;
        private System.Windows.Forms.Label lbHowToToken;
        private System.Windows.Forms.TextBox edToken;
        private System.Windows.Forms.Label lbToken;
        private System.Windows.Forms.TabPage tpgUserdata;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbUserdataHints;
        private System.Windows.Forms.CheckBox cbSavePassword;
        private System.Windows.Forms.CheckBox cbUseUserdata;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.TextBox edPassword;
        private System.Windows.Forms.TextBox edEmail;
    }
}