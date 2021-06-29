using System;

namespace VemVinner.Data.ComplexModel
{
    public class sp_getLatestGroupGameEvents_Result
    {
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public string Username { get; set; }
        public short Placement { get; set; }
    }
}
