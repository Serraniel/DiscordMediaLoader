using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DML.AppCore.Classes;
using SweetLib.Utils;
using SweetLib.Utils.Logger;

namespace DML.Application.Classes
{
    public class JobScheduler
    {
        private ulong messageScanned = 0;
        private ulong totalAttachments = 0;
        private ulong attachmentsDownloaded = 0;

        private bool Run { get; set; } = false;
        public List<Job> JobList { get; set; } = new List<Job>();
        public Dictionary<int, Queue<IMessage>> RunningJobs = new Dictionary<int, Queue<IMessage>>();
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

        public void Stop()
        {
            Run = false;
        }

        public void ScanAll()
        {
            Logger.Info("Started JobScheduler...");

            Logger.Debug("Entering job list handler loop...");
            //foreach (var job in JobList)
            for (var i = JobList.Count - 1; i >= 0; i--)
            {
                try
                {
                    var job = JobList[i];
                    Logger.Debug($"Checking job {job}");

                    Task.Run(async () =>
                    {
                        await job.Scan();
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
        }
    }
}
