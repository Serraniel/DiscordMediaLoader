using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using Discord.Net;
using Nito.AsyncEx;
using ConnectionState = Discord.ConnectionState;

namespace Discord_Media_Loader
{
    public partial class MainForm : Form
    {
        private DiscordClient Client { get; } = new DiscordClient();
        public MainForm()
        {
            InitializeComponent();
        }

        public async Task<bool> Login()
        {
            var email = Properties.Settings.Default.email;
            var abort = false;

            while (Client.State != ConnectionState.Connected && !abort)
            {
                var password = "";

                if (LoginForm.Exec(ref email, out password))
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        try
                        {
                            await Client.Connect(email, password);

                            Properties.Settings.Default.email = email;
                            Properties.Settings.Default.Save();
                        }
                        finally
                        {
                            Cursor = Cursors.Default;
                        }
                    }
                    catch (HttpException ex)
                    {
                        // ignore http exception on invalid login
                    }
                }
                else
                {
                    abort = true;
                }
            }

            return !abort;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            Enabled = false;

            if (!await Login())
            {
                Close();
            }
            else
            {
                foreach (var guild in Client.Servers)
                    cbGuilds.Items.Add(guild.Name);

                cbGuilds.SelectedIndex = 0;

                Enabled = true;
            }
        }

        private Server FindServerByName(string name)
        {
            return (from s in Client.Servers where s.Name == name select s).FirstOrDefault();
        }

        private Channel FindChannelByName(Server server, string name)
        {
            return (from c in server.TextChannels where c.Name == name select c).FirstOrDefault();
        }

        private void cbGuilds_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Server guild = FindServerByName(cbGuilds.Text);

                if (guild != null)
                {
                    cbChannels.Items.Clear();
                    foreach (var channel in guild.TextChannels)
                        cbChannels.Items.Add(channel.Name);

                    cbChannels.Text = guild.TextChannels.First()?.Name;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
