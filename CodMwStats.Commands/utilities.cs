using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodMwStats.ApiWrapper;
using CodMwStats.ApiWrapper.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace CodMwStats.Commands
{
    public class Utilities : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong");
        }
    }
}
