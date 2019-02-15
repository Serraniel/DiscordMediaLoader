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
 * Users who edited RpcWrapper.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DML.Application.Classes.RPC
{
    public class RpcWrapper
    {
        public const string Dll = "discord-rpc-win32";

        public RpcWrapper()
        {
            if (!File.Exists(Dll + ".dll"))
            {
                MessageBox.Show(
                    "Missing " + Dll + ".dll\n\n" +
                    "Grab it from the release on GitHub or from the DiscordRpcDemo/lib/ folder in the repo then put it alongside DiscordRpcDemo.exe.\n\n" +
                    "https://github.com/nostrenz/cshap-discord-rpc-demo"
                );

                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
