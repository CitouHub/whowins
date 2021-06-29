using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class Game
    {
        public Game()
        {
            GroupGameEvents = new HashSet<GroupGameEvent>();
            GroupGames = new HashSet<GroupGame>();
        }

        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfilePictureUrl { get; set; }

        public virtual ICollection<GroupGameEvent> GroupGameEvents { get; set; }
        public virtual ICollection<GroupGame> GroupGames { get; set; }
    }
}
