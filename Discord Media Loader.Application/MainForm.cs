using System.Linq;
using System.Windows.Forms;
using Discord;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application
{
    public partial class MainForm : Form
    {
        private bool IsInitialized { get; set; } = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            Trace("MainForm shown executed.");
            RefreshComponents();

            IsInitialized = true;
        }

        private void RefreshComponents()
        {
            Debug("Refreshing components...");

            Trace("Refreshing operating folder component...");
            edOperatingFolder.Text = Core.Settings.OperatingFolder;

            Trace("Refreshing name scheme component...");
            edNameScheme.Text = Core.Settings.FileNameScheme;

            Trace("Refreshing skip existing files component...");
            cbSkipExisting.Checked = Core.Settings.SkipExistingFiles;

            Trace("Refreshing thread limit component...");
            edThreadLimit.Value = Core.Settings.ThreadLimit;

            Trace("Adding guilds to component...");
            cbGuild.Items.AddRange(Core.Client.Servers.OrderBy(g => g.Name).Select(g => g.Name).ToArray());
            cbGuild.SelectedIndex = 0;
            Trace("Guild component initialized.");
        }

        private void DoSomethingChanged(object sender, System.EventArgs e)
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
            Core.Settings.ThreadLimit = (int)edThreadLimit.Value;

            Trace("Storing new settings...");
            Core.Settings.Store();

            Info("New settings have been saved.");
        }

        private void btnSearchFolders_Click(object sender, System.EventArgs e)
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

        private Server FindServerByName(string name)
        {
            Trace($"Trying to find server by name: {name}");
            return (from s in Core.Client.Servers where s.Name == name select s).FirstOrDefault();
        }

        private Channel FindChannelByName(Server server, string name)
        {
            Trace($"Trying to find channel in {server} by name: {name}");
            return (from c in server.TextChannels where c.Name == name select c).FirstOrDefault();
        }

        private Server FindServerById(ulong id)
        {
            Trace($"Trying to find server by Id: {id}");
            return (from s in Core.Client.Servers where s.Id == id select s).FirstOrDefault();
        }

        private Channel FindChannelById(Server server, ulong id)
        {
            Trace($"Trying to find channel in {server} by id: {id}");
            return (from c in server.TextChannels where c.Id == id select c).FirstOrDefault();
        }

        private void cbGuild_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Trace("Guild index changed.");
            Debug("Updating channel dropdown component...");

            UseWaitCursor = true;
            try
            {
                var guild = FindServerByName(cbGuild.Text);

                if (guild != null)
                {
                    Trace("Cleaning channel component from old values...");
                    cbChannel.Items.Clear();

                    Trace("Adding new channels...");
                    cbChannel.Items.AddRange(guild.TextChannels.OrderBy(c => c.Position).Select(c => c.Name).ToArray());
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
    }
}
