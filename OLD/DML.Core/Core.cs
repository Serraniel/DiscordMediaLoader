﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using DML.Core.Classes;

namespace DML.Core
{
    public static class Core
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static LiteDatabase Database { get; set; }
        internal static Settings Settings { get; set; }
        internal static JobScheduler Scheduler { get; } = new JobScheduler();

        internal static string DataDirectory
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Serraniel\Discord Media Loader");

        public static async Task Run(string[] paramStrings)
        {
            try
            {
                var splash = new FrmInternalSplash();
                splash.Show();
                System.Windows.Forms.Application.DoEvents();

                Info("Starting up Discord Media Loader application...");
                var useTrace = false;
#if DEBUG
                //temporary add debug log level if debugging...
                GlobalLogLevel |= LogLevel.Debug;
                Debug("Running in debug configuartion. Added log level debug.");
#endif

                Debug($"Parameters: {string.Join(", ", paramStrings)}");
                if (paramStrings.Contains("--trace") || paramStrings.Contains("-t"))
                {
                    useTrace = true;
                    GlobalLogLevel |= LogLevel.Trace;
                    Trace("Trace parameter found. Added log level trace.");
                }

                Debug($"Application data folder: {DataDirectory}");

                Trace("Checking application data folder...");
                if (!Directory.Exists(DataDirectory))
                {
                    Debug("Creating application data folder...");
                    Directory.CreateDirectory(DataDirectory);
                    Trace("Creating application data folder.");
                }

                Trace("Initializing profile optimizations...");
                ProfileOptimization.SetProfileRoot(System.Windows.Forms.Application.UserAppDataPath);
                ProfileOptimization.StartProfile("profile.opt");
                Trace("Finished initializing profile optimizations.");

                Trace("Trying to identify log memory...");
                var logMemory = DefaultLogMemory as ArchivableConsoleLogMemory;
                if (logMemory != null)
                {
                    var logFolder = Path.Combine(DataDirectory, "logs");
                    if (!Directory.Exists(logFolder))
                    {
                        Debug("Creating log folder...");
                        Directory.CreateDirectory(logFolder);
                        Trace("Created log folder.");
                    }


                    var logFile = Path.Combine(logFolder,
                        SweetUtils.LegalizeFilename($"{DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern)}.log.zip"));

                    Trace($"Setting log file: {logFile}");
                    logMemory.AutoArchiveOnDispose = true;
                    logMemory.ArchiveFile = logFile;
                }

                Debug("Loading database...");
                Database = new LiteDatabase(Path.Combine(DataDirectory, "config.db"));
                Database.Log.Logging += (message) => Trace($"LiteDB: {message}");

                Debug("Loading settings collection out of database...");
                var settingsDB = Database.GetCollection<Settings>("settings");
                if (settingsDB.Count() > 1)
                {
                    Warn("Found more than one setting. Loading first one...");
                }
                Settings = settingsDB.FindAll().FirstOrDefault();
                if (Settings == null)
                {
                    Warn("Settings not found. Creating new one. This is normal on first start up...");
                    Settings = new Settings();
                    Settings.Store();
                }

                Debug("Loading jobs collection out of database...");
                Scheduler.JobList = Job.RestoreJobs().ToList();

                Info("Loaded settings.");
                Debug(
                    $"Settings: Email: {Settings.Email}, password: {(string.IsNullOrEmpty(Settings.Password) ? "not set" : "is set")}, use username: {Settings.UseUserData}, loginToken: {Settings.LoginToken}");

                Trace("Updating log level...");
                GlobalLogLevel = Settings.ApplicactionLogLevel;
#if DEBUG
                //temporary add debug log level if debugging...
                GlobalLogLevel |= LogLevel.Debug;
                Debug("Running in debug configuartion. Added log level debug.");
#endif
                if (useTrace)
                {
                    GlobalLogLevel |= LogLevel.Trace;
                    Trace("Creating application data folder.");
                }

                Debug("Creating discord client...");
                
                Client = new DiscordSocketClient();
                Client.Log += (arg) =>
                {
                    var logMessage = $"DiscordClient: {arg.Message}";
                    switch (arg.Severity)
                    {
                        case LogSeverity.Verbose:
                            Trace(logMessage);
                            break;
                        case LogSeverity.Debug:
                            Trace(logMessage);
                            break;
                        case LogSeverity.Info:
                            Info(logMessage);
                            break;
                        case LogSeverity.Warning:
                            Warn(logMessage);
                            break;
                        case LogSeverity.Error:
                            Error(logMessage);
                            break;
                    }

                    return Task.CompletedTask;
                };


                Info("Trying to log into discord...");
                var abort = false;

                Client.Connected += Client_Connected;

                while (Client.LoginState != LoginState.LoggedIn && !abort)
                {
                    Debug(Client.ConnectionState.ToString());
                    Debug(Client.LoginState.ToString());

                    Trace("Entering login loop.");
                    
                    try
                    {
                        if (Client.ConnectionState == ConnectionState.Connecting)
                            continue;

                        if (!string.IsNullOrEmpty(Settings.LoginToken))
                        {
                            Debug("Trying to login with last known token...");
                            await Client.LoginAsync(TokenType.User, Settings.LoginToken);
                            await Task.Delay(1000);
                        }

                    }
                    catch (HttpException ex)
                    {
                        Warn($"Login seems to have failed or gone wrong: {ex.GetType().Name} - {ex.Message}");
                    }

                    if (Client.LoginState == LoginState.LoggedOut)
                    {
                        Settings.Password = string.Empty;
                        Debug("Showing dialog for username and password...");
                        var loginDlg = new LoginDialog();
                        loginDlg.ShowDialog();
                        Trace("Dialog closed.");
                    }
                }

                Debug("Start checking for invalid jobs...");

                //Client

                while (Client.Guilds.Count==0)
                {
                    // wait until guilds are loaded
                }

                for (var i = Scheduler.JobList.Count - 1; i >= 0; i--)
                {
                    var job = Scheduler.JobList[i];
                    var isError = false;
                    var guild = FindServerById(job.GuildId);
                    if (guild == null)
                        isError = true;
                    else
                    {
                        var channel = FindChannelById(guild, job.ChannelId);
                        if (channel == null)
                            isError = true;
                    }

                    if (isError)
                    {
                        MessageBox.Show($"Invalid job for guild {job.GuildId}, channel {job.ChannelId} found. Guild or channel may not exist any more. This job will be deleted...", "Invalid job",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        Scheduler.JobList.Remove(job);
                        Scheduler.RunningJobs.Remove(job.Id);
                        job.Stop();
                        job.Delete();
                    }
                }

                splash.Close();

                Info("Starting scheduler...");
                Scheduler.Start();

                System.Windows.Forms.Application.Run(new MainForm());

                Info("Stopping scheduler...");
                Scheduler.Stop();
            }
            catch (Exception ex)
            {
                Error($"{ex.Message} occured at: {ex.StackTrace}");
            }
        }

        private static Task Client_Connected()
        {
            Debug("Connected");
            return Task.CompletedTask;
        }

        //TODO: this is thrid time we implement this.....this has to be fixed!!!
        private static SocketGuild FindServerById(ulong id)
        {
            Trace($"Trying to find server by Id: {id}");
            return (from s in Core.Client.Guilds where s.Id == id select s).FirstOrDefault();
        }

        private static SocketTextChannel FindChannelById(SocketGuild server, ulong id)
        {
            Trace($"Trying to find channel in {server} by id: {id}");
            return (from c in server.TextChannels where c.Id == id select c).FirstOrDefault();
        }
    }
}
