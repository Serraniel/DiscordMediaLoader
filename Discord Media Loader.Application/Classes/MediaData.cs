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
 * Users who edited MediaData.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DML.Client;
using SweetLib.Utils.Logger;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Classes
{
    public class MediaData
    {
        public ulong Id { get; set; }
        public string DownloadSource { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public double TimeStamp { get; set; }
        public string Filename { get; set; }
        public int FileSize { get; set; }

        internal void Store()
        {
            Debug("Storing job to database...");
            Trace("Getting jobs collection...");
            var db = Core.Database.GetCollection<MediaData>("media");

            Trace("Adding new value...");

            if (db.Find(x => x.Id == Id).Any())
            {
                db.Update(this);
            }
            else
            {
                db.Insert(this);
            }
        }

        internal void Delete()
        {
            Debug("Deleting job from database...");
            Trace("Getting jobs collection...");
            var db = Core.Database.GetCollection<MediaData>("media");

            Trace("Deleting value...");
            db.Delete(Id);
        }

        internal async Task Download()
        {
            Trace("Beginning attachment download...");

            Debug("Building filename...");
            var fileName = Path.Combine(Core.Settings.OperatingFolder, Core.Settings.FileNameScheme);

            Debug($"Base filename: {fileName}");

            Trace("Determining if extension is required");
            var extensionRequired = !fileName.EndsWith("%name%");
            Trace($"Extension required: {extensionRequired}");

            Trace("Replacing filename placeholders...");
            var guildName = DMLClient.Client.GetGuild(GuildId)?.Name ?? GuildId.ToString();
            var channelName = DMLClient.Client.GetGuild(GuildId)?.GetChannel(ChannelId)?.Name ?? ChannelId.ToString();

            fileName =
                fileName.Replace("%guild%", guildName)
                    .Replace("%channel%", channelName)
                    .Replace("%timestamp%", TimeStamp.ToString("####"))
                    .Replace("%name%", Filename)
                    .Replace("%id%", Id.ToString());

            Trace("Adding extension if required");
            if (extensionRequired)
                fileName += Path.GetExtension(Filename);

            Debug($"Final filename: {fileName}");

            if (File.Exists(fileName) && new FileInfo(fileName).Length == FileSize)
            {
                Logger.Debug($"{fileName} already existing with its estimated size. Skipping...");
                return;
            }

            var wc = new WebClient();
            Logger.Debug($"Starting downloading of attachment {Id}...");
            wc.DownloadFile(new Uri(DownloadSource), fileName);
            Logger.Debug($"Downloaded attachment {Id}.");


            Core.Scheduler.AttachmentsDownloaded++;
        }
    }
}
