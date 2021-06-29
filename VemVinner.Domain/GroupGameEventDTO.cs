using System;
using System.Collections.Generic;

namespace VemVinner.Domain
{
    public class GroupGameEventDTO
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public List<GameUserScoreDTO> UserPlacements { get; set; }
    }
}
