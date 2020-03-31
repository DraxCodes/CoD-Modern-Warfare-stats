using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task StatsBN(string userName)
        {
            if (userName.Contains("#"))
            {
                userName.Replace('#', '%');
            }
            else
            {
                var errorEmbed = new EmbedBuilder();
                errorEmbed.WithTitle("Ouch! An error occurred.");
                errorEmbed.WithDescription("Invalid BattleNet username.");
                errorEmbed.WithColor(255, 0, 0);
                await Context.Channel.SendMessageAsync("", false, errorEmbed.Build());
            }

            var jsonAsString = await ApiProcessor.GetUser($"https://api.tracker.gg/api/v2/modern-warfare/standard/profile/battlenet/{userName}");
            var apiData = JsonConvert.DeserializeObject<ModerWarfareApiOutput>(jsonAsString);

            var name = apiData.Data.PlatformInfo.PlatformUserHandle;
            var pfp = apiData.Data.PlatformInfo.AvatarUrl;
            var playTime = apiData.Data.Segment[0].Stats.TimePlayedTotal.Value;
            var matches = apiData.Data.Segment[0].Stats.TotalGamesPlayed.Value;
            var levelImg = apiData.Data.Segment[0].Stats.Level.Metadata.IconUrl;
            var level = apiData.Data.Segment[0].Stats.Level.Value;
            var levelper = apiData.Data.Segment[0].Stats.LevelProgression.Value;
            var kd = apiData.Data.Segment[0].Stats.KdRatio.Value;
            var kills = apiData.Data.Segment[0].Stats.Kills.Value;
            var WinPer = apiData.Data.Segment[0].Stats.WlRatio.Value;
            var wins = apiData.Data.Segment[0].Stats.Wins.Value;
            var bestKillsreak = apiData.Data.Segment[0].Stats.LongestKillstreak.Value;
            var losses = apiData.Data.Segment[0].Stats.Losses.Value;
            var deaths = apiData.Data.Segment[0].Stats.Deaths.Value;
            var avgLife = apiData.Data.Segment[0].Stats.AverageLife.Value;
            var assists = apiData.Data.Segment[0].Stats.Assists.Value;
            var Score = apiData.Data.Segment[0].Stats.CareerScore.Value;

            var embed = new EmbedBuilder();
            embed.WithTitle($"Stats of {name}");
            embed.WithThumbnailUrl(pfp);
            embed.AddField("Play Time:", playTime);
            embed.AddField("Matches:", matches);
            embed.AddField("Level:", level);
            embed.AddField("Level Progression:", levelper);
            embed.WithImageUrl(levelImg.ToString());
            embed.AddField("K/D Ratio:", kd);
            embed.AddField("Kills:", kills);
            embed.AddField("Win %:", WinPer);
            embed.AddField("Wins:", wins);

            embed.AddField("Best Killstreak:", bestKillsreak);
            embed.AddField("Losses:", losses);
            embed.AddField("Deaths:", deaths);
            embed.AddField("Avg. Life:", avgLife);
            embed.AddField("Assists:", assists);
            embed.AddField("Score:", Score);


            embed.WithColor(new Color(239, 133, 141));

            var msg = await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
