#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2017-2019 - All Rights Reserved
 * 
 * This file is part of "DML.Application".
 * The official repository is hosted at https://github.com/Serraniel/DiscordMediaLoader
 * 
 * "DML.Application" is licensed under Apache 2.0.
 * Full license is included in the project repository.
 * 
 * Users who edited JobScheduler.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using Discord;
using Discord.WebSocket;
using DML.AppCore.Classes;
using SweetLib.Utils.Extensions;
using SweetLib.Utils.Logger;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DML.Application.Classes
{
    public class JobScheduler
    {
        private ulong messageScanned = 0;
        private ulong totalAttachments = 0;
        private ulong attachmentsDownloaded = 0;

        private bool Run { get; set; } = false;
        public List<Job> JobList { get; set; } = new List<Job>();
        public Dictionary<int, Queue<IMessage>> RunningJobs { get; } = new Dictionary<int, Queue<IMessage>>();
        internal int RunningThreads { get; set; } = 0;
        internal Task SchedulerTask { get; private set; }
        internal Task DownloadTask { get; private set; }

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

        public void StartScheduler()
        {
            Logger.Info("Starting scheduler jobs");
            SchedulerTask = Task.Run(() =>
            {
                PerformSchedulerTask();
            });

            DownloadTask = Task.Run(() =>
            {
                PerformDownloads();
            });
        }

        public void Stop()
        {
            Run = false;
        }

        private async void PerformSchedulerTask()
        {
            Logger.Trace("SchedulerTask started");
            foreach (var job in JobList)
            {
                if (job.State == JobState.Idle)
                {
                    // scan all old messages first
                    Logger.Debug($"Starting scan for job {job.Id}");
                    await job.Scan();
                    job.State = JobState.Listening; // set to listening now
                    Logger.Debug($"Scan for job {job.Id} finished");
                }
            }
            Logger.Trace("All jobs have been scanned");
        }

        private async void PerformDownloads()
        {
            Logger.Trace("SchedulerTask started");
            // TODO 
        }

        public Task HandleMessageReceived(SocketMessage msg)
        {
            Logger.Trace($"Received message: Id: {msg.Id}, Channel: {msg.Channel.Id}, Attachments: {msg.Attachments.Count}");

            if (msg.Attachments.Count > 0)
            {
                foreach (var job in JobList)
                {
                    if (job.State != JobState.Listening || job.ChannelId != msg.Channel.Id)
                    {
                        continue;
                    }

                    Logger.Debug($"Job found [{job.Id}]");
                    foreach (var attachment in msg.Attachments)
                    {
                        var mediaData = new MediaData
                        {
                            Id = attachment.Id,
                            GuildId = (msg.Channel as SocketTextChannel)?.Guild?.Id ?? 0,
                            ChannelId = msg.Channel.Id,
                            DownloadSource = attachment.Url,
                            Filename = attachment.Filename,
                            TimeStamp = msg.CreatedAt.UtcDateTime.ToUnixTimeStamp(),
                            FileSize = attachment.Size
                        };
                        mediaData.Store();
                    }

                    job.LastMessageId = msg.Id;
                    job.Store();
                }
            }

            return Task.CompletedTask;
        }
    }
}
