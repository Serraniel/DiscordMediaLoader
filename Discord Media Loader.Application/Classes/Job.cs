using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DML.Application.Classes;
using DML.Client;
using SweetLib.Utils;
using static SweetLib.Utils.Logger.Logger;

namespace DML.AppCore.Classes
{
    public class Job
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public double KnownTimestamp { get; set; } = 0;
        private double StopTimestamp { get; set; } = 0;
        private bool IsValid { get; set; } = true;

        internal void Store()
        {
            Debug("Storing job to database...");
            Trace("Getting jobs collection...");
            var jobDb = Core.Database.GetCollection<Job>("jobs");

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
            var jobDb = Core.Database.GetCollection<Job>("jobs");

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

        internal async Task<List<IMessage>> Scan()
        {
            Debug($"Starting scan of guild {GuildId} channel {ChannelId}...");
            var result = new List<IMessage>();

            var limit = 100;
            var lastId = ulong.MaxValue;
            var isFirst = true;
            var finished = false;

            var guild = FindServerById(GuildId);
            var channel = FindChannelById(guild, ChannelId);

            Debug("Checking channel access");
            if (!channel.Users.Contains(channel.Guild.CurrentUser))
            {
                Info("Skipping channel without access");
                return result;
            }

            if (Math.Abs(StopTimestamp) < 0.4)
                StopTimestamp = KnownTimestamp;
            Trace("Initialized scanning parameters.");

            while (!finished)
            {
                Trace("Entering scanning loop...");
                var messages = new List<IMessage>();

                Trace($"Downloading next {limit} messages...");
                if (isFirst)
                {
                    //messages = await channel.GetMessagesAsync(limit).ToArray() as SocketMessage[];
                    var realMessages = await channel.GetMessagesAsync(limit).ToArray();

                    foreach (var realMessageArray in realMessages)
                    {
                        foreach (var realMessage in realMessageArray)
                        {
                            messages.Add(realMessage);
                        }
                    }
                }
                else
                {
                    var realMessages = await channel.GetMessagesAsync(lastId, Direction.Before, limit).ToArray();

                    foreach (var realMessageArray in realMessages)
                    {
                        foreach (var realMessage in realMessageArray)
                        {
                            messages.Add(realMessage);
                        }
                    }

                    //messages = await channel.GetMessagesAsync(lastId, Direction.Before, limit).ToArray() as SocketMessage[];
                }
                Trace($"Downloaded {messages.Count} messages.");

                isFirst = false;

                foreach (var m in messages)
                {
                    if (!IsValid)
                        return null;

                    Debug($"Processing message {m.Id}");
                    if (m.Id < lastId)
                    {
                        Trace($"Updating lastId ({lastId}) to {m.Id}");
                        lastId = m.Id;
                    }

                    if (SweetUtils.DateTimeToUnixTimeStamp(m.CreatedAt.UtcDateTime) <= StopTimestamp)
                    {
                        Debug("Found a message with a known timestamp...Stopping scan.");
                        finished = true;
                        continue;
                    }

                    Trace($"Message {m.Id} has {m.Attachments.Count} attachments.");
                    if (m.Attachments.Count > 0)
                    {
                        result.Add(m);
                        Core.Scheduler.TotalAttachments += (ulong)m.Attachments.Count;
                        Trace($"Added message {m.Id}");
                    }
                    Debug($"Finished message {m.Id}");

                    Core.Scheduler.MessagesScanned++;
                }

                finished = finished || messages.Count < limit;
            }
            Trace($"Downloaded all messages for guild {GuildId} channel {ChannelId}.");

            Trace("Sorting messages...");
            result.Sort((a, b) => DateTime.Compare(a.CreatedAt.UtcDateTime, b.CreatedAt.UtcDateTime));

            if (result.Count > 0)
            {
                Trace("Updating StopTimestamp for next scan...");
                StopTimestamp = SweetUtils.DateTimeToUnixTimeStamp(result[result.Count - 1].CreatedAt.UtcDateTime);
            }

            Debug($"Fisnished scan of guild {GuildId} channel {ChannelId}.");

            return result;
        }

        public void Stop()
        {
            IsValid = false;
        }

        public static IEnumerable<Job> RestoreJobs()
        {
            Debug("Restoring jobs...");
            Trace("Getting jobs collection...");
            var jobDb = Core.Database.GetCollection<Job>("jobs");

            Trace("Creating new empty job list");
            return jobDb.FindAll();
        }
    }
}
