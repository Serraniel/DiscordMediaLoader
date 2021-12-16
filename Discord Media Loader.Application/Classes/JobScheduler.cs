using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DML.Application.Classes;
using SweetLib.Utils.Extensions;
using SweetLib.Utils.Logger;

namespace DML.AppCore.Classes
{
    public class JobScheduler
    {
        private ulong attachmentsDownloaded;
        private ulong messageScanned;
        public Dictionary<int, Queue<IMessage>> RunningJobs = new Dictionary<int, Queue<IMessage>>();
        private ulong totalAttachments;

        private bool Run { get; set; }
        public List<Job> JobList { get; set; } = new List<Job>();
        internal int RunningThreads { get; set; }

        internal ulong MessagesScanned
        {
            get
            {
                lock (this)
                {
                    return messageScanned;
                }
            }
            set
            {
                lock (this)
                {
                    messageScanned = value;
                }
            }
        }

        internal ulong TotalAttachments
        {
            get
            {
                lock (this)
                {
                    return totalAttachments;
                }
            }
            set
            {
                lock (this)
                {
                    totalAttachments = value;
                }
            }
        }

        internal ulong AttachmentsDownloaded
        {
            get
            {
                lock (this)
                {
                    return attachmentsDownloaded;
                }
            }
            set
            {
                lock (this)
                {
                    attachmentsDownloaded = value;
                }
            }
        }

        public void Stop()
        {
            Run = false;
        }

        public void Start()
        {
            Run = true;

            Task.Run(async () =>
            {
                Logger.Info("Started JobScheduler...");
                while (Run)
                    try
                    {
                        Logger.Debug("Entering job list handler loop...");
                        //foreach (var job in JobList)
                        for (var i = JobList.Count - 1; i >= 0; i--)
                        {
                            var job = JobList[i];
                            Logger.Debug($"Checking job {job}");
                            var hasJob = false;

                            Logger.Trace("Locking scheduler...");
                            lock (this)
                            {
                                Logger.Trace("Checking if job is already performed...");
                                hasJob = RunningJobs.ContainsKey(job.Id);
                            }

                            Logger.Trace("Unlocked scheduler.");

                            if (!hasJob)
                            {
                                Logger.Debug("Job is not performed yet...Performing job...");
                                var queue = new Queue<IMessage>();

                                Logger.Trace("Locking scheduler...");
                                lock (this)
                                {
                                    Logger.Trace("Adding job to running jobs.");
                                    RunningJobs.Add(job.Id, queue);
                                }

                                Logger.Trace("Unlocked scheduler.");

                                Logger.Trace("Issuing job message scan...");
                                var messages = await job.Scan();

                                if (messages == null)
                                    continue;

                                Logger.Trace($"Adding {messages.Count} messages to queue...");
                                foreach (var msg in messages) queue.Enqueue(msg);
                                Logger.Trace($"Added {queue.Count} messages to queue.");

                                if (messages.Count != queue.Count)
                                    Logger.Warn("Not all messages have been added into the queue.");

                                var startedDownload = false;

                                while (!startedDownload)
                                {
                                    Logger.Debug("Entering loop to check thread availability");
                                    Logger.Trace("Locking scheduler...");
                                    lock (this)
                                    {
                                        Logger.Trace(
                                            $"Checking thread limit. Running: {RunningThreads}, Max: {Core.Settings.ThreadLimit}");
                                        if (RunningThreads >= Core.Settings.ThreadLimit)
                                            continue;

                                        RunningThreads++;
                                        startedDownload = true;
                                    }

                                    Logger.Trace("Unlocked scheduler.");
                                }

                                Logger.Trace("Start downloading job async.");
                                Task.Run(() => WorkQueue(job.Id)); // do not await to work parallel
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
            });
        }

        private void WorkQueue(int jobId)
        {
            try
            {
                Logger.Debug("Beginning job download...");
                Logger.Trace("Finding job...");
                var job = (from j in JobList where j.Id == jobId select j).FirstOrDefault();

                if (job == null)
                {
                    Logger.Warn($"Associating job not found! JobId: {jobId}");
                    return;
                }

                Logger.Trace("Found job.");

                Queue<IMessage> queue;
                Logger.Trace("Locking scheduler...");
                lock (this)
                {
                    Logger.Trace("Finiding queue...");
                    if (!RunningJobs.TryGetValue(jobId, out queue))
                    {
                        Logger.Warn($"Queue for job {jobId} not found!");
                        return;
                    }

                    Logger.Trace("Queue found.");
                }

                Logger.Trace("Unlocked scheduler.");

                Logger.Debug($"Messages to process for job {jobId}: {queue.Count}");
                while (queue.Count > 0)
                {
                    Logger.Trace("Locking scheduler...");
                    lock (this)
                    {
                        Logger.Trace("Checking if still a job...");
                        if (!RunningJobs.ContainsKey(jobId))
                        {
                            Logger.Warn($"Queue for job {jobId} not found!");
                            return;
                        }

                        Logger.Trace("Continue working...");
                    }

                    Logger.Trace("Unlocked scheduler.");

                    Logger.Trace("Dequeueing message...");
                    var message = queue.Dequeue();

                    Logger.Debug($"Attachments for message {message.Id}: {message.Attachments.Count}");
                    foreach (var a in message.Attachments)
                        try
                        {
                            var fileName = Path.Combine(Core.Settings.OperatingFolder, Core.Settings.FileNameScheme);

                            Logger.Trace("Replacing filename placeholders...");

                            var extensionRequired = !fileName.EndsWith("%name%");

                            var serverName = "unknown";
                            string nickname = null;

                            var socketTextChannel = message.Channel as SocketTextChannel;
                            if (socketTextChannel != null)
                            {
                                serverName = socketTextChannel.Guild.Name;
                                serverName = Path.GetInvalidFileNameChars()
                                    .Aggregate(serverName, (current, c) => current.Replace(c, ' '));

                                var serverUser =
                                    socketTextChannel.GetUser(message.Author.Id); // can be null if user left the server
                                if (serverUser != null)
                                    nickname = serverUser.Nickname;
                            }

                            var channelName = message.Channel.Name;
                            channelName = Path.GetInvalidFileNameChars()
                                .Aggregate(channelName, (current, c) => current.Replace(c, ' '));

                            fileName =
                                fileName.Replace("%guild%", serverName)
                                    .Replace("%channel%", channelName)
                                    .Replace("%timestamp%", message.CreatedAt.UtcDateTime.ToUnixTimeStamp().ToString())
                                    .Replace("%name%", a.Filename)
                                    .Replace("%id%", a.Id.ToString())
                                    .Replace("%userid%", message.Author.Id.ToString())
                                    .Replace("%username%", message.Author.Username)
                                    .Replace("%nickname%",
                                        !string.IsNullOrEmpty(nickname) ? nickname : message.Author.Username);

                            if (extensionRequired)
                                fileName += Path.GetExtension(a.Filename);

                            Logger.Trace($"Detemined file name: {fileName}.");


                            if (File.Exists(fileName) && new FileInfo(fileName).Length == a.Size)
                            {
                                Logger.Debug($"{fileName} already existing with its estimated size. Skipping...");
                                continue;
                            }

                            Logger.Trace("Determining directory...");
                            var fileDirectory = Path.GetDirectoryName(fileName);

                            if (!Directory.Exists(fileDirectory))
                            {
                                Logger.Info($"Directory {fileDirectory} does not exist. Creating directory...");
                                Directory.CreateDirectory(fileDirectory);
                                Logger.Debug("Created directory.");
                            }

                            var wc = new WebClient();
                            Logger.Debug($"Starting downloading of attachment {a.Id}...");

                            wc.DownloadFile(new Uri(a.Url), fileName);
                            Logger.Debug($"Downloaded attachment {a.Id}.");

                            Logger.Trace("Updating known timestamp for job...");
                            job.KnownTimestamp = message.CreatedAt.UtcDateTime.ToUnixTimeStamp();
                            job.Store();
                        }
                        finally
                        {
                            AttachmentsDownloaded++;
                        }
                }
            }
            finally
            {
                Logger.Trace("Locking scheduler...");
                lock (this)
                {
                    Logger.Trace($"Removing {jobId} from running jobs...");
                    RunningJobs.Remove(jobId);
                    Logger.Trace("Decreasing thread count...");
                    RunningThreads--;
                }

                Logger.Trace("Unlocked scheduler.");
            }
        }
    }
}