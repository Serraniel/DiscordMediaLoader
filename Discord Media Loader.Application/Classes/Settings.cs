using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweetLib.Utils.Logger;
using static SweetLib.Utils.Logger.Logger;

namespace DML.Application.Classes
{
    internal class Settings
    {
        public int Id { get; } = 1; // using always unique ID
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginToken { get; set; }
        public bool UseUserData { get; set; } = false;
        public bool SavePassword { get; set; } = false;
        public LogLevel ApplicactionLogLevel { get; set; } = LogLevel.Info | LogLevel.Warn | LogLevel.Error;

        internal void Store()
        {
            Trace("Getting settings collection...");
            var settingsDB = Core.Database.GetCollection<Settings>("settings");

            Debug("Storing settings to database...");

            if (settingsDB.Exists(_setting => _setting.Id == Id))
            {
                Trace("Updating existing value...");
                settingsDB.Update(this);
            }
            else
            {
                Trace("Adding new value...");
                settingsDB.Insert(this);
            }
        }
    }
}
