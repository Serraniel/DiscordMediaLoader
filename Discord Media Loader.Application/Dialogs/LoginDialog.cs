using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            edEmail.Text = Core.Settings.Email;
            edPassword.Text = Core.Settings.Password;
            cbUseUserdata.Checked = Core.Settings.UseUserData;
            cbSavePassword.Checked = Core.Settings.SavePassword;

            tbcLoginMethods.SelectedTab = Core.Settings.UseUserData ? tpgUserdata : tpgToken;
        }

        private void LoginDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace($"Closing login dialog. Result: {DialogResult}");
            if (DialogResult != DialogResult.OK)
                return;

            Debug("Adjusting login settings...");
            Core.Settings.LoginToken = edToken.Text;
            Core.Settings.Email = edEmail.Text;
            Core.Settings.Password = edPassword.Text;
            Core.Settings.UseUserData = cbUseUserdata.Checked;
            Core.Settings.SavePassword = cbSavePassword.Checked;

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
