using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class GroupGame
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int GroupId { get; set; }
        public int GameId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Game Game { get; set; }
        public virtual Group Group { get; set; }
    }
}
