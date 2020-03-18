using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AccountLogic.ServerAccounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace CoDModernWarfareStats.Main
{
    class Program : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            string filePath = "Resources/Ascii.json";

            if (File.Exists(filePath))
            {
                string ascii = File.ReadAllText(filePath);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(ascii);
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (string.IsNullOrEmpty(Config.bot.token)) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.IntitializeAsync(_client);
            await Task.Delay(-1);

        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
