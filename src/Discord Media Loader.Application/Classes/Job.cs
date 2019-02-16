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
using DML.Application.Core;
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
        public ulong LastMessageId { get; set; }
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
            /*
            Debug($"Starting scan of guild {GuildId} channel {ChannelId}...");
            var result = new List<IMessage>();

            var limit = 100;
            var lastId = ulong.MaxValue;
            var isFirst = true;
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

            if (Math.Abs(StopTimestamp) < 0.4)
            {
                StopTimestamp = KnownTimestamp;
            }

            Trace("Initialized scanning parameters.");

            while (!finished)
            {
                Trace("Entering scanning loop...");
                var messages = new List<IMessage>();

                Trace($"Downloading next {limit} messages...");
                if (isFirst)
                {
                    var realMessages = await channel.GetMessagesAsync(limit).ToArray();

                    messages.AddRange(realMessages.SelectMany(realMessageArray => realMessageArray));
                }
                else
                {
                    var realMessages = await channel.GetMessagesAsync(lastId, Direction.Before, limit).ToArray();

                    messages.AddRange(realMessages.SelectMany(realMessageArray => realMessageArray));
                }

                Trace($"Downloaded {messages.Count} messages.");
                isFirst = false;

                foreach (var m in messages)
                {
                    if (!IsValid)
                    {
                        return false;
                    }

                    Application.Core.Core.Scheduler.MessagesScanned++;

                    Debug($"Processing message {m.Id}");
                    if (m.Id < lastId)
                    {
                        Trace($"Updating lastId ({lastId}) to {m.Id}");
                        lastId = m.Id;
                    }

                    if (m.CreatedAt.UtcDateTime.ToUnixTimeStamp() <= StopTimestamp)
                    {
                        Debug("Found a message with a known timestamp...Stopping scan.");
                        finished = true;
                        continue;
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

                finished = finished || messages.Count < limit;
            }

            Trace($"Downloaded all messages for guild {GuildId} channel {ChannelId}.");

            Trace("Sorting messages...");
            result.Sort((a, b) => DateTime.Compare(a.CreatedAt.UtcDateTime, b.CreatedAt.UtcDateTime));

            foreach (var r in result)
            {
                foreach (var a in r.Attachments)
                {
                    var mediaData = new MediaData
                    {
                        Id = a.Id,
                        GuildId = (r.Channel as SocketTextChannel)?.Guild?.Id ?? 0,
                        ChannelId = r.Channel.Id,
                        DownloadSource = a.Url,
                        Filename = a.Filename,
                        TimeStamp = r.CreatedAt.UtcDateTime.ToUnixTimeStamp(),
                        FileSize = a.Size
                    };
                    mediaData.Store();
                }
            }

            if (result.Count > 0)
            {
                Trace("Updating StopTimestamp for next scan...");
                StopTimestamp = result[result.Count - 1].CreatedAt.UtcDateTime.ToUnixTimeStamp();
                KnownTimestamp = StopTimestamp;
                Store();
                return false;
            }
            else
            {
                // if we found any messages we remember the timestamp of starting so we don´t have to scan all past messages....
                StopTimestamp = scanStartTimeStamp.ToUnixTimeStamp();
                KnownTimestamp = StopTimestamp;
                Store();

                var realLastMessage = await channel.GetMessagesAsync(1).ToArray();
                return scanStartTimeStamp > (realLastMessage.SelectMany(realLastMessageArray => realLastMessageArray)
                                                 .FirstOrDefault()?.CreatedAt.UtcDateTime ??
                                             scanStartTimeStamp);
            }

            Debug($"Fisnished scan of guild {GuildId} channel {ChannelId}.");
            */
            return false;
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
