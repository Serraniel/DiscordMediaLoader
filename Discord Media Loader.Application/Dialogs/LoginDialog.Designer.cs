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
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbHowToToken = new System.Windows.Forms.Label();
            this.edToken = new System.Windows.Forms.TextBox();
            this.lbToken = new System.Windows.Forms.Label();
            this.pnlButtons.SuspendLayout();
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
            // lbHowToToken
            // 
            this.lbHowToToken.Location = new System.Drawing.Point(7, 58);
            this.lbHowToToken.Name = "lbHowToToken";
            this.lbHowToToken.Size = new System.Drawing.Size(412, 87);
            this.lbHowToToken.TabIndex = 5;
            this.lbHowToToken.Text = resources.GetString("lbHowToToken.Text");
            // 
            // edToken
            // 
            this.edToken.Location = new System.Drawing.Point(79, 12);
            this.edToken.Name = "edToken";
            this.edToken.Size = new System.Drawing.Size(335, 20);
            this.edToken.TabIndex = 4;
            // 
            // lbToken
            // 
            this.lbToken.AutoSize = true;
            this.lbToken.Location = new System.Drawing.Point(7, 15);
            this.lbToken.Name = "lbToken";
            this.lbToken.Size = new System.Drawing.Size(66, 13);
            this.lbToken.TabIndex = 3;
            this.lbToken.Text = "Login token:";
            // 
            // LoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 219);
            this.Controls.Add(this.lbHowToToken);
            this.Controls.Add(this.edToken);
            this.Controls.Add(this.lbToken);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbHowToToken;
        private System.Windows.Forms.TextBox edToken;
        private System.Windows.Forms.Label lbToken;
    }
}