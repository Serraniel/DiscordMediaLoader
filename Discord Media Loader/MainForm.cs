using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
                cbGuilds.Items.AddRange((from g in Client.Servers orderby g.Name select g.Name).ToArray());

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
                    cbChannels.Items.AddRange((from c in guild.TextChannels orderby c.Position select c.Name).ToArray());

                    cbChannels.SelectedIndex = 0;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void cbLimitDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpLimit.Enabled = cbLimitDate.Checked;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tbxPath.Text = dlg.SelectedPath;
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            var path = tbxPath.Text;
            var useStopDate = cbLimitDate.Checked;
            var stopDate = dtpLimit.Value;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter an existing directory.");
                return;
            }

            Enabled = false;

            var guild = FindServerByName(cbGuilds.Text);
            var channel = FindChannelByName(guild, cbChannels.Text);

            var clients = new List<WebClient>();

            var limit = 100;
            var stop = false;
            ulong lastId = ulong.MaxValue;
            var isFirst = true;

            while (!stop)
            {
                Discord.Message[] messages;

                if (isFirst)
                    messages = await channel.DownloadMessages(limit, null, Relative.Before, true);
                else
                    messages = await channel.DownloadMessages(limit, lastId, Relative.Before, true);

                isFirst = false;

                foreach (var m in messages)
                {
                    if (m.Id < lastId)
                        lastId = m.Id;

                    if (useStopDate && m.Timestamp < stopDate.Date)
                    {
                        stop = true;
                        continue;
                    }

                    foreach (var a in m.Attachments)
                    {
                        var wc = new WebClient();
                        clients.Add(wc);

                        wc.DownloadFileCompleted += (wcSender, wcE) => clients.Remove((WebClient)wcSender);
                        wc.DownloadFile(new Uri(a.Url), $@"{path}\{a.Filename}");
                    }
                }

                stop = stop || messages.Length < limit;
            }

            await Task.Run(() =>
            {
                while (clients.Count > 0)
                {
                    // wait until download finished
                }
            });

            Enabled = true;
            Process.Start(path);
        }
    }
}
