using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class BaseEntityDTO
    {
        [DataMember]
        public int Id { get; set; }

        protected bool Equals(BaseEntityDTO other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntityDTO) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}