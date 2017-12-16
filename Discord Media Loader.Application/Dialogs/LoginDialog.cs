using System;
using System.Windows.Forms;
using DML.Application.Classes;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Dialogs
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void LoginDialog_Shown(object sender, EventArgs e)
        {
            Trace("Login dialog shown.");
            edToken.Text = Core.Settings.LoginToken;
        }

        private void LoginDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace($"Closing login dialog. Result: {DialogResult}");
            if (DialogResult != DialogResult.OK)
                return;

            Debug("Adjusting login settings...");
            Core.Settings.LoginToken = edToken.Text;

            Core.Settings.Store();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Trace("btnOk pressed.");
            DialogResult = DialogResult.OK;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            Trace("btnAbort pressed.");
            DialogResult = DialogResult.Abort;
        }
    }
}
