using System;

namespace RemoteNotes.DAL.Domain.Entities
{
    public class Account : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public byte[] Photo { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}