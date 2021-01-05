using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class UserDTO : BaseEntityDTO
    {
        [DataMember]
        public AccountDTO Account { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [ExcludeFromCodeCoverage]
        protected bool Equals(UserDTO other)
        {
            return base.Equals(other) && Equals(Account, other.Account) && Login == other.Login &&
                   Password == other.Password && IsActive == other.IsActive;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UserDTO) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Account != null ? Account.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Login != null ? Login.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsActive.GetHashCode();
                return hashCode;
            }
        }
    }
}
