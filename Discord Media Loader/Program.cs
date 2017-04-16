using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord_Media_Loader.Helper;
using DML.Application;

namespace Discord_Media_Loader
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProfileOptimization.SetProfileRoot(Application.UserAppDataPath);
            ProfileOptimization.StartProfile("profile.opt");

            var splashScreen = new FrmSplash();
            splashScreen.ShowDialog();

            Core.Run();
        }
    }
}
