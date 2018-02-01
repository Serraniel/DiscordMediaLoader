using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DML.AppCore.Classes;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Classes
{
    public class MediaData
    {
        public ulong Id { get; set; }
        public string DownloadSource { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set;}
        public double TimeStamp { get; set; }
        public string Filename { get; set; }

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
    }
}
