using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class GroupGameEventUserResult
    {
        public long Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int GroupGameEventId { get; set; }
        public int UserId { get; set; }
        public short Placement { get; set; }

        public virtual GroupGameEvent GroupGameEvent { get; set; }
        public virtual User User { get; set; }
    }
}
