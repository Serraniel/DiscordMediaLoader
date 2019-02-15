#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2019-2019 - All Rights Reserved
 * 
 * This file is part of "Discord Media Loader".
 * The official repository is hosted at https://github.com/Serraniel/Darkorbit-Helper-Program
 * 
 * "Discord Media Loader" is licensed under European Union Public Licence V. 1.2.
 * Full license is included in the project repository.
 * 
 * Users who edited Program.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

using System;
using System.Windows.Forms;
using DML.Application.Classes;
using Nito.AsyncEx;

namespace Discord_Media_Loader
{
    static class Program
    {
        [STAThread]
        static void Main(string[] paramStrings)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var splashScreen = new FrmSplash();
            if (splashScreen.ShowDialog() == DialogResult.OK)
            {
               DoLaunch(paramStrings);
            }
            else
            {
                Application.Restart();
            }
        }

        private static void DoLaunch(string[] paramStrings)
        {
            AsyncContext.Run(() => Core.Run(paramStrings));
        }
    }
}
