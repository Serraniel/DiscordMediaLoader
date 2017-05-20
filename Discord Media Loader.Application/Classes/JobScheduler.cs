using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using SweetLib.Utils;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Classes
{
    internal class JobScheduler
    {
        private ulong messageScanned = 0;
        private ulong totalAttachments = 0;
        private ulong attachmentsDownloaded = 0;

        private bool Run { get; set; } = false;
        internal List<Job> JobList { get; set; } = new List<Job>();
        internal Dictionary<int, Queue<Message>> RunningJobs = new Dictionary<int, Queue<Message>>();
        internal int RunningThreads { get; set; } = 0;

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

        internal ulong AttachmentsToDownload => TotalAttachments - AttachmentsDownloaded;

        internal void Stop()
        {
            Run = false;
        }

        internal void Start()
        {
            Run = true;

            Task.Run(async () =>
            {
                Info("Started JobScheduler...");
                while (Run)
                {
                    Debug("Entering job list handler loop...");
                    //foreach (var job in JobList)
                    for(var i = JobList.Count-1;i>=0;i--)
                    {
                        var job = JobList[i];
                        Debug($"Checking job {job}");
                        var hasJob = false;

                        Trace("Locking scheduler...");
                        lock (this)
                        {
                            Trace("Checking if job is already performed...");
                            hasJob = RunningJobs.ContainsKey(job.Id);
                        }
                        Trace("Unlocked scheduler.");

                        if (!hasJob)
                        {
                            Debug("Job is not performed yet...Performing job...");
                            var queue = new Queue<Message>();

                            Trace("Locking scheduler...");
                            lock (this)
                            {
                                Trace("Adding job to running jobs.");
                                RunningJobs.Add(job.Id, queue);
                            }
                            Trace("Unlocked scheduler.");

                            Trace("Issuing job message scan...");
                            var messages = await job.Scan();

                            if(messages==null)
                                continue;

                            Trace($"Adding {messages.Count} messages to queue...");
                            foreach (var msg in messages)
                            {
                                queue.Enqueue(msg);
                            }
                            Trace($"Added {queue.Count} messages to queue.");

                            if (messages.Count != queue.Count)
                                Warn("Not all messages have been added into the queue.");

                            var startedDownload = false;

                            while (!startedDownload)
                            {
                                Debug("Entering loop to check thread availability");
                                Trace("Locking scheduler...");
                                lock (this)
                                {
                                    Trace($"Checking thread limit. Running: {RunningThreads}, Max: {Core.Settings.ThreadLimit}");
                                    if (RunningThreads >= Core.Settings.ThreadLimit)
                                        continue;

                                    RunningThreads++;
                                    startedDownload = true;
                                }
                                Trace("Unlocked scheduler.");
                            }

                            Trace("Start downloading job async.");
                            Task.Run(() => WorkQueue(job.Id)); // do not await to work parallel
                        }
                    }
                }
            });
        }

        private void WorkQueue(int jobId)
        {
            try
            {
                Debug("Beginning job download...");
                Trace("Finding job...");
                var job = (from j in JobList where j.Id == jobId select j).FirstOrDefault();

                if (job == null)
                {
                    Warn($"Associating job not found! JobId: {jobId}");
                    return;
                }
                Trace("Found job.");

                Queue<Message> queue;
                Trace("Locking scheduler...");
                lock (this)
                {
                    Trace("Finiding queue...");
                    if (!RunningJobs.TryGetValue(jobId, out queue))
                    {
                        Warn($"Queue for job {jobId} not found!");
                        return;
                    }
                    Trace("Queue found.");
                }
                Trace("Unlocked scheduler.");

                Debug($"Messages to process for job {jobId}: {queue.Count}");
                while (queue.Count > 0)
                {
                    Trace("Locking scheduler...");
                    lock (this)
                    {
                        Trace("Checking if still a job...");
                        if (!RunningJobs.ContainsKey(jobId))
                        {
                            Warn($"Queue for job {jobId} not found!");
                            return;
                        }
                        Trace("Continue working...");
                    }
                    Trace("Unlocked scheduler.");

                    Trace("Dequeueing message...");
                    var message = queue.Dequeue();

                    Debug($"Attachments for message {message.Id}: {message.Attachments.Length}");
                    foreach (var a in message.Attachments)
                    {
                        try
                        {
                            var fileName = Path.Combine(Core.Settings.OperatingFolder, Core.Settings.FileNameScheme);

                            Trace("Replacing filename placeholders...");

                            var extensionRequired = !fileName.EndsWith("%name%");

                            fileName =
                                fileName.Replace("%guild%", message.Server.Name.Replace(":","").Replace("/","").Replace("\\",""))
                                    .Replace("%channel%", message.Channel.Name)
                                    .Replace("%timestamp%", SweetUtils.DateTimeToUnixTimeStamp(message.Timestamp).ToString())
                                    .Replace("%name%", a.Filename)
                                    .Replace("%id%", a.Id);

                            if (extensionRequired)
                                fileName += Path.GetExtension(a.Filename);

                            Trace($"Detemined file name: {fileName}.");
                            

                            if (File.Exists(fileName) && new FileInfo(fileName).Length == a.Size)
                            {
                                Debug($"{fileName} already existing with its estimated size. Skipping...");
                                continue;
                            }

                            Trace("Determining directory...");
                            var fileDirectory = Path.GetDirectoryName(fileName);

                            if (!Directory.Exists(fileDirectory))
                            {
                                Info($"Directory {fileDirectory} does not exist. Creating directory...");
                                Directory.CreateDirectory(fileDirectory);
                                Debug("Created directory.");
                            }

                            var wc = new WebClient();
                            Debug($"Starting downloading of attachment {a.Id}...");

                            wc.DownloadFile(new Uri(a.Url), fileName);
                            Debug($"Downloaded attachment {a.Id}.");

                            Trace("Updating known timestamp for job...");
                            job.KnownTimestamp = SweetUtils.DateTimeToUnixTimeStamp(message.Timestamp);
                            job.Store();
                        }
                        finally
                        {
                            AttachmentsDownloaded++;
                        }
                    }
                }
            }
            finally
            {
                Trace("Locking scheduler...");
                lock (this)
                {
                    Trace($"Removing {jobId} from running jobs...");
                    RunningJobs.Remove(jobId);
                    Trace("Decreasing thread count...");
                    RunningThreads--;
                }
                Trace("Unlocked scheduler.");
            }
        }
    }
}
