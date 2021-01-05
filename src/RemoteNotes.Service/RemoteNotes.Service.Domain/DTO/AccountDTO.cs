using System;
using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class AccountDTO : BaseEntityDTO
    {
        [DataMember]
        public DateTime CreateTime { get; set; }

        [DataMember]
        public DateTime ModifyTime { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Nickname { get; set; }

        [DataMember]
        public DateTime Birthday { get; set; }

        [DataMember]
        public string Email { get; set; }

        protected bool Equals(AccountDTO other)
        {
            return base.Equals(other) && CreateTime.Equals(other.CreateTime) && ModifyTime.Equals(other.ModifyTime) &&
                   Equals(Photo, other.Photo) && FirstName == other.FirstName && LastName == other.LastName &&
                   Nickname == other.Nickname && Birthday.Equals(other.Birthday) && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AccountDTO) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ CreateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ModifyTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (Photo != null ? Photo.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Nickname != null ? Nickname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Birthday.GetHashCode();
                hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
