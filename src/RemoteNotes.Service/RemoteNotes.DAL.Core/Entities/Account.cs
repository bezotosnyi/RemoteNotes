using System;

namespace RemoteNotes.DAL.Core.Entities
{
    public class Account : BaseEntity
    {
        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public byte[] Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}