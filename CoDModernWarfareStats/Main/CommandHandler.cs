using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AccountLogic.ServerAccounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CoDModernWarfareStats.Main
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        private async void setPlayStatus()
        {
            await _client.SetGameAsync($"@Nano Help");
            System.Threading.Thread.Sleep(5000);
            await _client.SetGameAsync("先輩", "", Discord.ActivityType.Listening);
            System.Threading.Thread.Sleep(5000);
            await _client.SetGameAsync($"Working on a update which allows custom prefix ...");
            System.Threading.Thread.Sleep(5000);
            setPlayStatus();
        }

        public async Task IntitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            _client.MessageReceived += HandleCommandAsync;
            _client.JoinedGuild += JoinedServer;
            setPlayStatus();
        }


        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            if (context.User.IsBot) return;

            var ServerPrefix = "";
            if (!(context.Channel is IDMChannel))
            {
                ServerPrefix = ServerAccounts.GetAccount((SocketGuild)context.Guild).Prefix;
            }
            else
            {
                ServerPrefix = ">";
            }

            int argPos = 0;
            if (msg.HasStringPrefix(ServerPrefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, null);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(DateTimeOffset.UtcNow.ToString("[d.MM.yyyy HH:mm:ss] "));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.ErrorReason);
                    Console.ForegroundColor = ConsoleColor.White;

                    var embed = new EmbedBuilder();
                    embed.WithTitle("Ouch! something went wrong senpai.");
                    embed.WithDescription(result.ErrorReason);
                    embed.WithColor(255, 0, 0);
                    await context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }

        public async Task JoinedServer(SocketGuild Guild)
        {
            var target = Guild;
            ServerAccounts.GetAccount(target);
        }
    }
}
