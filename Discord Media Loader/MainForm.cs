using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using Discord.Net;
using ConnectionState = Discord.ConnectionState;

namespace Discord_Media_Loader
{
    public partial class MainForm : Form
    {
        private DiscordClient Client { get; } = new DiscordClient();
        private event EventHandler<UpdateProgessEventArgs> UpdateProgress;
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public MainForm()
        {
            InitializeComponent();

            UpdateProgress += (s, e) =>
            {
                SetControlPropertyThreadSafe(lbDownload, "Text", $"Files downloaded: {e.Downloaded}");
                SetControlPropertyThreadSafe(lbScanCount, "Text", $"Messages scanned: {e.Scanned}");
            };
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        private static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), control, propertyName, propertyValue);

            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new[] { propertyValue });
            }
        }

        public async Task<bool> Login()
        {
            var email = Properties.Settings.Default.email;
            var abort = false;

            while (Client.State != ConnectionState.Connected && !abort)
            {
                string password;

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
                    catch (HttpException)
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
            lbVersion.Text = $"v{Assembly.GetExecutingAssembly().GetName().Version}";

            SetEnabled(false);

            if (!await Login())
            {
                Close();
            }
            else
            {
                cbGuilds.Items.AddRange((from g in Client.Servers orderby g.Name select g.Name).ToArray());
                cbGuilds.SelectedIndex = 0;
                lbUsername.Text = $"Username: {Client.CurrentUser.Name}#{Client.CurrentUser.Discriminator}";

                SetEnabled(true);
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

        private void SetEnabled(bool enabled)
        {
            foreach (Control c in Controls)
            {
                SetControlPropertyThreadSafe(c, "Enabled", enabled);
                //c.Enabled = enabled;
            }
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

        private void OnUpdateProgress(UpdateProgessEventArgs e)
        {
            EventHandler<UpdateProgessEventArgs> handler = UpdateProgress;
            handler?.Invoke(this, e);
        }

        private static long DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            TimeSpan elapsedTime = dateTime - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            var path = tbxPath.Text;
            var useStopDate = cbLimitDate.Checked;
            var stopDate = dtpLimit.Value;
            var threadLimit = nupThreadCount.Value;
            var skipExisting = cbSkip.Checked;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Please enter an existing directory.");
                return;
            }

            SetEnabled(false);

            var guild = FindServerByName(cbGuilds.Text);
            var channel = FindChannelByName(guild, cbChannels.Text);

            var clients = new List<WebClient>();

            var limit = 100;
            var stop = false;
            var lastId = ulong.MaxValue;
            var isFirst = true;

            ulong msgScanCount = 0;
            ulong downloadCount = 0;
            var locker = new object();

            Task.Run(async () =>
            {

                while (!stop)
                {
                    Discord.Message[] messages;

                    if (isFirst)
                        messages = await channel.DownloadMessages(limit, null);
                    else
                        messages = await channel.DownloadMessages(limit, lastId);

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
                            if (!path.EndsWith(@"\"))
                                path += @"\";

                            var fname = $"{guild.Name}_{channel.Name}_{DateTimeToUnixTimeStamp(m.Timestamp)}_{a.Filename}";
                            fname = Path.GetInvalidFileNameChars().Aggregate(fname, (current, c) => current.Replace(c, '-'));
                            fname = path + fname;

                            if (skipExisting && File.Exists(fname))
                                continue;

                            while (clients.Count >= threadLimit)
                            {
                                // wait
                            }
                            var wc = new WebClient();
                            clients.Add(wc);

                            wc.DownloadFileCompleted += (wcSender, wcE) =>
                            {
                                clients.Remove(wc);
                                lock (locker)
                                {
                                    downloadCount++;
                                    OnUpdateProgress(new UpdateProgessEventArgs() { Downloaded = downloadCount, Scanned = msgScanCount });
                                }
                            };

                            wc.DownloadFileAsync(new Uri(a.Url), fname);
                        }

                        msgScanCount++;
                        OnUpdateProgress(new UpdateProgessEventArgs() { Downloaded = downloadCount, Scanned = msgScanCount});
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

                Process.Start(path);
                SetEnabled(true);
            });
        }

        private void lbGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Serraniel/DiscordMediaLoader/releases");
        }

        private void lbAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.AboutString);
        }
    }

    internal class UpdateProgessEventArgs : EventArgs
    {
        internal ulong Scanned { get; set; }
        internal ulong Downloaded { get; set; }
    }
}
