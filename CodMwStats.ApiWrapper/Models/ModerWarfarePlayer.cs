using System;

namespace CodMwStats.ApiWrapper.Models
{
    public class ModerWarfarePlayer
    {
        public string Name { get; }
        public Uri ProfilePicURl { get; }
        public double? Playtime { get; }
        public double? Matches { get; }
        public Uri LevelImageUrl { get;  }
        public double? Level { get; }
        public double? LevelProgression { get; }
        public double? KillDeathRatio { get; }
        public double? Kills { get; }
        public double? WinLossRation { get; }
        public double? Wins { get; }
        public double? Losses { get; }
        public double? BestKillStreak { get; }
        public double? Deaths { get; }
        public double? AverageLife { get; }
        public double? Assists { get; }
        public double? Score { get; }

        public ModerWarfarePlayer(ModerWarfareApiOutput apiOutput)
        {
            Name = apiOutput.Data.PlatformInfo.PlatformUserHandle;
            ProfilePicURl = new Uri(apiOutput.Data.PlatformInfo.AvatarUrl);
            Playtime = apiOutput.Data.Segment[0].Stats.TotalGamesPlayed.Value;
            LevelImageUrl = apiOutput.Data.Segment[0].Stats.Level.Metadata.IconUrl;
            Level = apiOutput.Data.Segment[0].Stats.Level.Value;
            LevelProgression = apiOutput.Data.Segment[0].Stats.LevelProgression.Value;
            KillDeathRatio = apiOutput.Data.Segment[0].Stats.KdRatio.Value;
            Kills = apiOutput.Data.Segment[0].Stats.Kills.Value;
            WinLossRation = apiOutput.Data.Segment[0].Stats.WlRatio.Value;
            Wins = apiOutput.Data.Segment[0].Stats.Wins.Value;
            BestKillStreak = apiOutput.Data.Segment[0].Stats.LongestKillstreak.Value;
            Losses = apiOutput.Data.Segment[0].Stats.Losses.Value;
            Deaths = apiOutput.Data.Segment[0].Stats.Deaths.Value;
            AverageLife = apiOutput.Data.Segment[0].Stats.AverageLife.Value;
            Assists = apiOutput.Data.Segment[0].Stats.Assists.Value;
            Score = apiOutput.Data.Segment[0].Stats.CareerScore.Value;
        }
    }
}
