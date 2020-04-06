using System.Threading.Tasks;
using CodMwStats.ApiWrapper;
using CodMwStats.ApiWrapper.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace CodMwStats.Commands.MainStatsCommands
{
    public class BattleNetStats : ModuleBase<SocketCommandContext>
    {
        [Command("Stats-battlenet")]
        [Alias("StatsBN")]
        public async Task StatsBN([Remainder] string userName)
        {
            if (userName.Contains("#"))
            {
                userName = userName.Replace('#', '%');
            }
            else
            {
                var errorEmbed = new EmbedBuilder();
                errorEmbed.WithTitle("Ouch! An error occurred.");
                errorEmbed.WithDescription("Invalid BattleNet username.");
                errorEmbed.WithColor(255, 0, 0);
                await Context.Channel.SendMessageAsync("", false, errorEmbed.Build());
                return;
            }

            var jsonAsString = await ApiProcessor.GetUser($"https://api.tracker.gg/api/v2/modern-warfare/standard/profile/battlenet/{userName}");
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
