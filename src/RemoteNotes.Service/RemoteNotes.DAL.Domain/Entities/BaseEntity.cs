using System.Diagnostics.CodeAnalysis;

namespace RemoteNotes.DAL.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [ExcludeFromCodeCoverage]
        protected bool Equals(BaseEntity other)
        {
            return Id == other.Id;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return Id;
        }
    }
}