using SweetLib.Utils.Logger;

namespace DML.Application.Classes
{
    public class Settings
    {
        public int Id { get; } = 1; // using always unique ID
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginToken { get; set; }
        public bool UseUserData { get; set; } = false;
        public bool SavePassword { get; set; } = false;
        public LogLevel ApplicactionLogLevel { get; set; } = LogLevel.Info | LogLevel.Warn | LogLevel.Error;
        public string OperatingFolder { get; set; }
        public string FileNameScheme { get; set; } = @"%guild%\%channel%\%id%";
        public bool SkipExistingFiles { get; set; } = true;
        public int ThreadLimit { get; set; } = 50;
        public bool RescanRequired { get; set; } = true;

        public void Store()
        {
            Logger.Trace("Getting settings collection...");
            var settingsDB = Core.Database.GetCollection<Settings>("settings");

            Logger.Debug("Storing settings to database...");

            if (settingsDB.Exists(_setting => _setting.Id == Id))
            {
                Logger.Trace("Updating existing value...");
                settingsDB.Update(this);
            }
            else
            {
                Logger.Trace("Adding new value...");
                settingsDB.Insert(this);
            }
        }
    }
}
