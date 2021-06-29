﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class UserAchievement
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int UserId { get; set; }
        public short AchievementId { get; set; }

        public virtual Achievement Achievement { get; set; }
        public virtual User User { get; set; }
    }
}
