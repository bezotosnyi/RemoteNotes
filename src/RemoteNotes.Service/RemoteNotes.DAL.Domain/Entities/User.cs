using System.Diagnostics.CodeAnalysis;

namespace RemoteNotes.DAL.Domain.Entities
{
    public class User : BaseEntity
    {
        public Account Account { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        [ExcludeFromCodeCoverage]
        protected bool Equals(User other)
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
            return Equals((User) obj);
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