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
 * Users who edited Migrator.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion


using System;
using System.Collections.Generic;
using DML.AppCore.Classes;
using LiteDB;

namespace DML.Application.Core
{
    internal static class Migrator
    {
        internal static ushort Version => 1;

        internal static void CheckMigrations()
        {
            var baseVersion = Core.Database.Engine.UserVersion;

            for (var step = (ushort)(baseVersion + 1); step <= Version; step++)
            {
                Migrate(step);
            }
        }

        private static void Migrate(ushort step)
        {
            switch (step)
            {
                case 0:
                    // base database
                    break;
                case 1:
                    foreach (var jobDoc in Core.Database.Engine.Find("jobs", Query.All()))
                    {
                        // pseudo datetime snowflake conversion https://discordapp.com/developers/docs/reference#convert-snowflake-to-datetime
                        var pseudoId = 0UL;
                        var timestamp = (ulong)jobDoc["KnownTimestamp"].AsDouble * 1000; // milliseconds have not been stored

                        if (timestamp > 0)
                        {
                            pseudoId = timestamp - 1420070400000 << 22;
                            pseudoId = pseudoId - (1000UL * 60 * 60 * 24 << 22); // substract one random day of pseudo id just in case the timestamp has errors
                        }
                        
                        jobDoc["LastMessageId"] = Convert.ToInt64(pseudoId); // LiteDB maps (u)long to Int64
                        jobDoc.Remove("KnownTimestamp");

                        Core.Database.Engine.Update("jobs", jobDoc);
                    }

                    break;
            }

            Core.Database.Engine.UserVersion = step;
        }
    }
}
