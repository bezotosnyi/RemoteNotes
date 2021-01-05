using System;
using System.Diagnostics.CodeAnalysis;

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

        [ExcludeFromCodeCoverage]
        protected bool Equals(Account other)
        {
            return base.Equals(other) && UserId == other.UserId && Equals(User, other.User) &&
                   FirstName == other.FirstName && LastName == other.LastName && Nickname == other.Nickname &&
                   Email == other.Email && Birthday.Equals(other.Birthday) && Equals(Photo, other.Photo) &&
                   CreateTime.Equals(other.CreateTime) && ModifyTime.Equals(other.ModifyTime);
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Account) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId;
                hashCode = (hashCode * 397) ^ (User != null ? User.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Nickname != null ? Nickname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Birthday.GetHashCode();
                hashCode = (hashCode * 397) ^ (Photo != null ? Photo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ModifyTime.GetHashCode();
                return hashCode;
            }
        }
    }
}