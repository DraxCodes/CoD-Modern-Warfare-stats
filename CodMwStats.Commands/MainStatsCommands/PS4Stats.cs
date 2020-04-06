using System.Threading.Tasks;
using CodMwStats.ApiWrapper;
using CodMwStats.ApiWrapper.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace CodMwStats.Commands.MainStatsCommands
{
    public class PS4Stats : ModuleBase<SocketCommandContext>
    {
        [Command("Stats-psn")]
        [Alias("Statspsn")]
        public async Task StatsPSN([Remainder] string userName)
        {
            var jsonAsString = await ApiProcessor.GetUser($"https://api.tracker.gg/api/v2/modern-warfare/standard/profile/psn/{userName}");
            var apiData = JsonConvert.DeserializeObject<ModerWarfareApiOutput>(jsonAsString);

            var player = new ModerWarfarePlayer(apiData);

            var embed = new EmbedBuilder()
                .WithTitle($"Stats of {player.Name}")
                .WithThumbnailUrl(player.ProfilePicURl.AbsoluteUri)
                .AddField("Play Time:", player.Playtime)
                .AddField("Matches:", player.Matches)
                .AddField("Level:", player.Level)
                .AddField("Level Progression:", player.LevelProgression)
                .WithImageUrl(player.LevelImageUrl.AbsoluteUri)
                .AddField("K/D Ratio:", player.KillDeathRatio)
                .AddField("Kills:", player.Kills)
                .AddField("Win %:", player.WinLossRation)
                .AddField("Wins:", player.Wins)
                .AddField("Best Killstreak:", player.BestKillStreak)
                .AddField("Losses:", player.Losses)
                .AddField("Deaths:", player.Deaths)
                .AddField("Avg. Life:", player.AverageLife)
                .AddField("Assists:", player.Assists)
                .AddField("Score:", player.Score)
                .WithColor(new Color(239, 133, 141));

            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
