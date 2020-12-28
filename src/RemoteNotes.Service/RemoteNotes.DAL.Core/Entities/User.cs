namespace RemoteNotes.DAL.Core.Entities
{
    public class User : BaseEntity
    {
        public Account Account { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}