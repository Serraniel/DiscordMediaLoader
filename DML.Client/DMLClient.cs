using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DML.Client
{
    public static class DMLClient
    {
        public static DiscordSocketClient Client { get; set; } = new DiscordSocketClient(new DiscordSocketConfig(){DefaultRetryMode = RetryMode.RetryRatelimit|RetryMode.RetryTimeouts});

        public static async Task<bool> Login(string token)
        {
            await Client.LoginAsync(TokenType.User, token);
            await Client.StartAsync();
            await Task.Delay(1000);

            while (Client.LoginState == LoginState.LoggingIn || Client.ConnectionState == ConnectionState.Connecting)
            {
                // wait
            }

            return Client.LoginState == LoginState.LoggedIn && Client.ConnectionState == ConnectionState.Connected;
        }
    }
}
