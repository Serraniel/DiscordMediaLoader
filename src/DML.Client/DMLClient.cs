#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2017-2019 - All Rights Reserved
 * 
 * This file is part of "DML.Client".
 * The official repository is hosted at https://github.com/Serraniel/DiscordMediaLoader
 * 
 * "DML.Client" is licensed under Apache 2.0.
 * Full license is included in the project repository.
 * 
 * Users who edited DMLClient.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

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