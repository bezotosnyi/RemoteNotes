using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RemoteNotes.Service.Domain.DTO
{
    [DataContract]
    public class NoteDTO : BaseEntityDTO
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public AccountDTO Account { get; set; }

        [DataMember]
        public DateTime PublishTime { get; set; }

        [DataMember]
        public DateTime ModifyTime { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string Text { get; set; }

        [ExcludeFromCodeCoverage]
        protected bool Equals(NoteDTO other)
        {
            return base.Equals(other) && Title == other.Title && Equals(Account, other.Account) &&
                   PublishTime.Equals(other.PublishTime) && ModifyTime.Equals(other.ModifyTime) &&
                   Equals(Image, other.Image) && Text == other.Text;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NoteDTO) obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Account != null ? Account.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PublishTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ModifyTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (Image != null ? Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
