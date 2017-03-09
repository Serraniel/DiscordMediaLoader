using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_Media_Loader
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProfileOptimization.SetProfileRoot(Application.UserAppDataPath);
            ProfileOptimization.StartProfile("profile.opt");

            //Application.Run(new FrmDownload());
        }
    }
}
