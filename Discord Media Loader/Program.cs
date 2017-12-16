using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord_Media_Loader.Helper;
using DML.Application;
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
