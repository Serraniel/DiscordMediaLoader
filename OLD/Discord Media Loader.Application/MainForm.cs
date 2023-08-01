﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Discord;
using Discord.WebSocket;
using DML.AppCore.Classes;
using DML.Application.Classes;
using DML.Application.Helper;
using DML.Application.Properties;
using DML.Client;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application
{
    internal enum OnlineState
    {
        Online,
        Idle,
        DoNotDisturb,
        Invisible
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool IsInitialized { get; set; }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Trace("MainForm shown executed.");
            RefreshComponents();

            IsInitialized = true;
        }

        private void RefreshComponents()
        {
            Debug("Refreshing components...");

            lbVersion.Text = $"v{Assembly.GetExecutingAssembly().GetName().Version} Copyright © by Serraniel";

            Trace("Refreshing operating folder component...");
            edOperatingFolder.Text = Core.Settings.OperatingFolder;

            Trace("Refreshing name scheme component...");
            edNameScheme.Text = Core.Settings.FileNameScheme;

            Trace("Refreshing skip existing files component...");
            cbSkipExisting.Checked = Core.Settings.SkipExistingFiles;

            Trace("Refreshing thread limit component...");
            edThreadLimit.Value = Core.Settings.ThreadLimit;

            if (cbGuild.Items.Count == 0)
            {
                Trace("Adding guilds to component...");

                cbGuild.Items.AddRange(DMLClient.Client.Guilds.Where(g => g.Name != null).OrderBy(g => g.Name)
                    .Select(g => new IdentifiedString<ulong>(g.Id, g.Name)).ToArray());

                cbGuild.SelectedIndex = 0;
                Trace("Guild component initialized.");
            }

            Trace("Refreshing job list component...");
            var oldIndex = lbxJobs.SelectedIndex;
            lbxJobs.Items.Clear();
            foreach (var job in Core.Scheduler.JobList)
                lbxJobs.Items.Add(new IdentifiedString<int>(job.Id,
                    $"{FindServerById(job.GuildId)?.Name}:{FindChannelById(FindServerById(job.GuildId), job.ChannelId)?.Name}"));
            lbxJobs.SelectedIndex = oldIndex;

            lbStatus.Text = DMLClient.Client.CurrentUser.Status.ToString();
        }

        private void DoSomethingChanged(object sender, EventArgs e)
        {
            Debug($"DoSomethingChanged excuted by {sender}.");
            if (!IsInitialized)
            {
                Trace("Form not initialized. Leaving DoSomethingChanged...");
                return;
            }

            Trace("Updating operating folder...");
            Core.Settings.OperatingFolder = edOperatingFolder.Text;

            Trace("Updating name scheme...");
            Core.Settings.FileNameScheme = edNameScheme.Text;

            Trace("Updating skip existing files...");
            Core.Settings.SkipExistingFiles = cbSkipExisting.Checked;

            Trace("Updating thread limit...");
            Core.Settings.ThreadLimit = (int) edThreadLimit.Value;

            Trace("Storing new settings...");
            Core.Settings.Store();

            Info("New settings have been saved.");
        }

        private void btnSearchFolders_Click(object sender, EventArgs e)
        {
            Trace("Operating folder button pressed.");
            using (var browserDialog = new FolderBrowserDialog())
            {
                Debug("Showing file browser dialog for operating folder...");

                browserDialog.SelectedPath = edOperatingFolder.Text;
                browserDialog.ShowNewFolderButton = true;
                browserDialog.Description = "Select an operating folder...";

                if (browserDialog.ShowDialog() == DialogResult.OK)
                {
                    edOperatingFolder.Text = browserDialog.SelectedPath;
                    Debug("Updated operating folder.");
                }
            }
        }

        private SocketGuild FindServerByName(string name)
        {
            Trace($"Trying to find server by name: {name}");
            return (from s in DMLClient.Client.Guilds where s.Name == name select s).FirstOrDefault();
        }

        private SocketTextChannel FindChannelByName(SocketGuild server, string name)
        {
            Trace($"Trying to find channel in {server} by name: {name}");
            return (from c in server.TextChannels where c.Name == name select c).FirstOrDefault();
        }

        private SocketGuild FindServerById(ulong id)
        {
            Trace($"Trying to find server by Id: {id}");
            return (from s in DMLClient.Client.Guilds where s.Id == id select s).FirstOrDefault();
        }

        private SocketTextChannel FindChannelById(SocketGuild server, ulong id)
        {
            Trace($"Trying to find channel in {server} by id: {id}");
            return (from c in server.TextChannels where c.Id == id select c).FirstOrDefault();
        }

        private void cbGuild_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trace("Guild index changed.");
            Debug("Updating channel dropdown component...");

            UseWaitCursor = true;
            try
            {
                var guild = FindServerById(((IdentifiedString<ulong>) cbGuild.SelectedItem).Id);

                if (guild != null)
                {
                    Trace("Cleaning channel component from old values...");
                    cbChannel.Items.Clear();

                    Trace("Adding new channels...");

                    cbChannel.Items.AddRange(guild.TextChannels.OrderBy(c => c.Position)
                        .Select(c => new IdentifiedString<ulong>(c.Id, c.Name)).ToArray());

                    Trace($"Added {cbChannel.Items.Count} channels.");

                    cbChannel.SelectedIndex = 0;
                }
                else
                {
                    Warn($"Guild {cbGuild.Text} could not be found!");
                }
            }
            finally
            {
                UseWaitCursor = false;
            }

            Debug("Finished updating channel dropdown component.");
        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            var job = new Job
            {
                GuildId = ((IdentifiedString<ulong>) cbGuild.SelectedItem).Id,
                ChannelId = ((IdentifiedString<ulong>) cbChannel.SelectedItem).Id
            };

            if (!(from j in Core.Scheduler.JobList
                where j.GuildId == job.GuildId && j.ChannelId == job.ChannelId
                select j).Any())
            {
                job.Store();
                Core.Scheduler.JobList.Add(job);
            }

            RefreshComponents();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Trace("Deleting job pressed.");

            if (lbxJobs.SelectedIndex < 0)
            {
                Warn("No job selected.");
                MessageBox.Show("No job has been seleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var jobId = ((IdentifiedString<int>) lbxJobs.SelectedItem).Id;

            var job = Core.Scheduler.JobList.FirstOrDefault(j => j.Id == jobId);
            if (job != null)
            {
                Core.Scheduler.JobList.Remove(job);
                Core.Scheduler.RunningJobs.Remove(job.Id);
                job.Stop();
                job.Delete();
            }

            lbxJobs.SelectedIndex = -1;
            RefreshComponents();
        }

        private void tmrRefreshProgress_Tick(object sender, EventArgs e)
        {
            var scanned = Core.Scheduler.MessagesScanned;
            var totalAttachments = Core.Scheduler.TotalAttachments;
            var done = Core.Scheduler.AttachmentsDownloaded;

            var progress = totalAttachments > 0 ? (int) (100 * done / totalAttachments) : 0;
            progress = Math.Min(Math.Max(0, progress), 100);
            pgbProgress.Maximum = 100;
            pgbProgress.Value = progress;

            lbProgress.Text = $"Scanned: {scanned} Downloaded: {done} Open: {totalAttachments - done}";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.AboutString);
        }

        private void visitGithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Serraniel/DiscordMediaLoader/");
        }

        private async void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var state = (OnlineState) Convert.ToInt32(e.ClickedItem.Tag);

            lbStatus.Text = state.ToString();
            tmrTriggerRefresh.Start();

            switch (state)
            {
                case OnlineState.Online:
                    await DMLClient.Client.SetStatusAsync(UserStatus.Online);
                    break;
                case OnlineState.Idle:
                    await DMLClient.Client.SetStatusAsync(UserStatus.Idle);
                    break;
                case OnlineState.DoNotDisturb:
                    await DMLClient.Client.SetStatusAsync(UserStatus.DoNotDisturb);
                    break;
                case OnlineState.Invisible:
                    await DMLClient.Client.SetStatusAsync(UserStatus.Invisible);
                    break;
            }
        }

        private void tmrTriggerRefresh_Tick(object sender, EventArgs e)
        {
            lbStatus.Text = DMLClient.Client.CurrentUser.Status.ToString();
            tmrTriggerRefresh.Stop();
        }

        private void showTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Core.Settings.LoginToken);
            MessageBox.Show(this, "Your login token has been copied to your clipboard.", "Discord Media Loader",
                MessageBoxButtons.OK);
        }

        private void btnFileNameHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.FileNameInfo);
        }
    }
}