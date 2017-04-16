using Discord;

namespace DML.Application
{
    public class Core
    {
        internal static DiscordClient Client;

        public static void Run()
        {
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
