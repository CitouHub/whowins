namespace VemVinner.Data.ComplexModel
{
    public class sp_getGroups_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool InvitationAccepted { get; set; }
        public int UsersInGroup { get; set; }
    }
}
