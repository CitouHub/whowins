using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class User
    {
        public User()
        {
            GroupGameEventUserResults = new HashSet<GroupGameEventUserResult>();
            GroupUsers = new HashSet<GroupUser>();
            UserAchievements = new HashSet<UserAchievement>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<GroupGameEventUserResult> GroupGameEventUserResults { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
