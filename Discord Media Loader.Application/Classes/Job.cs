using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using SweetLib.Utils;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Classes
{
    public class Job
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public double KnownTimestamp { get; set; } = 0;
        private double StopTimestamp { get; set; } = 0;

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

        internal async Task<List<Message>> Scan()
        {
            Debug($"Starting scan of guild {GuildId} channel {ChannelId}...");
            var result = new List<Message>();

            var limit = 100;
            var lastId = ulong.MaxValue;
            var isFirst = true;
            var finished = false;

            var guild = FindServerById(GuildId);
            var channel = FindChannelById(guild, ChannelId);

            if (Math.Abs(StopTimestamp) < 0.4)
                StopTimestamp = KnownTimestamp;
            Trace("Initialized scanning parameters.");

            while (!finished)
            {
                Trace("Entering scanning loop...");
                Message[] messages;

                Trace($"Downloading next {limit} messages...");
                if (isFirst)
                {
                    messages = await channel.DownloadMessages(limit, null);
                }
                else
                {
                    messages = await channel.DownloadMessages(limit, lastId);
                }
                Trace($"Downloaded {messages.Length} messages.");

                isFirst = false;

                foreach (var m in messages)
                {
                    Debug($"Processing message {m.Id}");
                    if (m.Id < lastId)
                    {
                        Trace($"Updating lastId ({lastId}) to {m.Id}");
                        lastId = m.Id;
                    }

                    if (SweetUtils.DateTimeToUnixTimeStamp(m.Timestamp) < StopTimestamp)
                    {
                        Debug("Found a message with a known timestamp...Stopping scan.");
                        finished = true;
                        continue;
                    }

                    Trace($"Message {m.Id} has {m.Attachments.Length} attachments.");
                    if (m.Attachments.Length > 0)
                    {
                        result.Add(m);
                        Trace($"Added message {m.Id}");
                    }
                    Debug($"Finished message {m.Id}");
                }

                finished = finished || messages.Length < limit;
            }
            Trace($"Downloaded all messages for guild {GuildId} channel {ChannelId}.");

            Trace("Sorting messages...");
            result.Sort((a, b) => DateTime.Compare(a.Timestamp, b.Timestamp));

            Trace("Updating StopTimestamp for next scan...");
            StopTimestamp = SweetUtils.DateTimeToUnixTimeStamp(result[result.Count - 1].Timestamp);

            Debug($"Fisnished scan of guild {GuildId} channel {ChannelId}.");

            return result;
        }

        internal static IEnumerable<Job> RestoreJobs()
        {
            Debug("Restoring jobs...");
            Trace("Getting jobs collection...");
            var jobDb = Core.Database.GetCollection<Job>("jobs");

            Trace("Creating new empty job list");
            return jobDb.FindAll();
        }
    }
}
