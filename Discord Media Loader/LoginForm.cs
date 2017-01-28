using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_Media_Loader
{
    public partial class LoginForm : Form
    {
        public static bool Exec(ref string email, out string password)
        {
            var loginForm = new LoginForm { tbxEmail = { Text = email } };
            password = "";

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                email = loginForm.tbxEmail.Text;
                password = loginForm.tbxPassword.Text;
                return true;
            }

            return false;
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void tbx_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                DialogResult = DialogResult.OK;
            else if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Abort;
        }
    }
}
