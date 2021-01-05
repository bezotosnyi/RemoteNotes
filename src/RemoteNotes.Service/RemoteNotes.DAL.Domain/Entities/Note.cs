using System;
using System.Diagnostics.CodeAnalysis;

namespace RemoteNotes.DAL.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Note : BaseEntity
    {
        public int UserId { get; set; }
        public Account Account { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public DateTime PublishTime { get; set; }

        public DateTime ModifyTime { get; set; }

        protected bool Equals(Note other)
        {
            return base.Equals(other) && UserId == other.UserId && Equals(Account, other.Account) &&
                   Title == other.Title && Text == other.Text && Equals(Image, other.Image) &&
                   PublishTime.Equals(other.PublishTime) && ModifyTime.Equals(other.ModifyTime);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Note) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId;
                hashCode = (hashCode * 397) ^ (Account != null ? Account.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Image != null ? Image.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PublishTime.GetHashCode();
                hashCode = (hashCode * 397) ^ ModifyTime.GetHashCode();
                return hashCode;
            }
        }
    }
}