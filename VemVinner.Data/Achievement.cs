using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class Achievement
    {
        public Achievement()
        {
            UserAchievements = new HashSet<UserAchievement>();
        }

        public short Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
