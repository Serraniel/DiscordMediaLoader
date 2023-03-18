using System;
using System.Windows.Forms;
using DML.Application.Classes;

namespace Discord_Media_Loader
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] paramStrings)
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);

            var splashScreen = new FrmSplash();
            if (splashScreen.ShowDialog() == DialogResult.OK)
                DoLaunch(paramStrings);
            else
                Application.Restart();
        }

        private static void DoLaunch(string[] paramStrings)
        {
            Core.Run(paramStrings);
        }
    }
}
