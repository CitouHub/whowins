using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class UserStatistic
    {
        public int UserId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int LoggedIn { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesFirstPlace { get; set; }
        public short GamesFirstPlaceStreak { get; set; }
        public int GamesSecondPlace { get; set; }
        public short GamesSecondPlaceStreak { get; set; }
        public int GamesThirdPlace { get; set; }
        public short GamesThirdPlaceStreak { get; set; }
        public int GamesLastPlace { get; set; }
        public short GamesLastPlaceStreak { get; set; }
    }
}
