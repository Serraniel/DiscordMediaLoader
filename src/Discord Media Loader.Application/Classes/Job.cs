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
 * Users who edited Job.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using Discord;
using Discord.WebSocket;
using DML.Application.Classes;
using DML.Client;
using SweetLib.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SweetLib.Utils.Logger.Logger;

namespace DML.AppCore.Classes
{
    internal enum JobState
    {
        Idle,
        Scanning,
        Listening
    }

    public class Job
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public ulong LastMessageId { get; set; } = 0;
        private bool IsValid { get; set; } = true;
        internal JobState State { get; set; } = JobState.Idle;


        internal void Store()
        {
            Debug("Storing job to database...");
            Trace("Getting jobs collection...");
            var jobDb = Application.Core.Core.Database.GetCollection<Job>("jobs");

            Trace("Adding new value...");

            if (jobDb.Find(x => x.ChannelId == ChannelId && x.GuildId == GuildId).Any())
            {
                jobDb.Update(this);
            }
            else
            {
                jobDb.Insert(this);
            }
        }

        public void Delete()
        {
            Debug("Deleting job from database...");
            Trace("Getting jobs collection...");
            var jobDb = Application.Core.Core.Database.GetCollection<Job>("jobs");

            Trace("Deleting value...");
            jobDb.Delete(Id);
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

        /// <summary>
        /// Performs scanning task of the job.
        /// </summary>
        /// <returns>Returns true if the newest messages have been scanned.</returns>
        internal async Task<bool> Scan()
        {
            Debug($"Starting scan of guild {GuildId} channel {ChannelId}...");
            var result = new List<IMessage>();
            const ushort limit = 100;
            State = JobState.Scanning;

            var finished = false;
            var scanStartTimeStamp = DateTime.UtcNow;

            var guild = FindServerById(GuildId);
            var channel = FindChannelById(guild, ChannelId);

            Debug("Checking channel access");
            if (channel.GetUser(channel.Guild.CurrentUser.Id) == null)
            {
                Info("Skipping channel without access");
                return true;
            }

            Trace("Initialized scanning parameters.");
            while (!finished)
            {
                Trace("Entering scanning loop...");
                var messages = new List<IMessage>();

                Trace($"Downloading next {limit} messages...");
                messages.AddRange((await channel.GetMessagesAsync(LastMessageId, Direction.After, limit).ToArray()).SelectMany(collection => collection));

                Debug($"Downloaded {messages.Count} messages.");
                Trace("Iterating messages...");
                foreach (var m in messages)
                {
                    if (!IsValid)
                    {
                        return false;
                    }

                    Application.Core.Core.Scheduler.MessagesScanned++;

                    Debug($"Processing message {m.Id}");
                    if (m.Id > LastMessageId)
                    {
                        Trace($"Updating lastId ({LastMessageId}) to {m.Id}");
                        LastMessageId = m.Id;
                    }

                    Trace($"Message {m.Id} has {m.Attachments.Count} attachments.");
                    if (m.Attachments.Count > 0)
                    {
                        result.Add(m);
                        Application.Core.Core.Scheduler.TotalAttachments += (ulong)m.Attachments.Count;
                        Trace($"Added message {m.Id}");
                    }
                    Debug($"Finished message {m.Id}");
                }

                finished = messages.Count < limit;
            }

            Trace($"Downloaded all messages for guild {GuildId} channel {ChannelId}.");

            Trace("Sorting messages...");
            result.Sort((a, b) => DateTime.Compare(a.CreatedAt.UtcDateTime, b.CreatedAt.UtcDateTime));

            foreach (var message in result)
            {
                foreach (var attachment in message.Attachments)
                {
                    var mediaData = new MediaData
                    {
                        Id = attachment.Id,
                        GuildId = (message.Channel as SocketTextChannel)?.Guild?.Id ?? 0,
                        ChannelId = message.Channel.Id,
                        DownloadSource = attachment.Url,
                        Filename = attachment.Filename,
                        TimeStamp = message.CreatedAt.UtcDateTime.ToUnixTimeStamp(),
                        FileSize = attachment.Size
                    };
                    mediaData.Store();
                }
            }
           

            Debug($"Fisnished scan of guild {GuildId} channel {ChannelId}.");
            return true;
        }

        public void Stop()
        {
            IsValid = false;
        }

        public static IEnumerable<Job> RestoreJobs()
        {
            Debug("Restoring jobs...");
            Trace("Getting jobs collection...");
            var jobDb = Application.Core.Core.Database.GetCollection<Job>("jobs");

            Trace("Creating new empty job list");
            return jobDb.FindAll();
        }
    }
}
