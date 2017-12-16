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
