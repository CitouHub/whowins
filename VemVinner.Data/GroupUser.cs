using System;
using System.Collections.Generic;

#nullable disable

namespace VemVinner.Data
{
    public partial class GroupUser
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertByUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateByUser { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public bool InvitationAccepted { get; set; }
        public bool? IsActive { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
