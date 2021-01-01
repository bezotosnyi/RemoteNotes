using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.Tests.Fixture
{
    public class TestEntity : BaseEntity
    {
        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((TestEntity) obj);
        }

        public override int GetHashCode() => (Title != null ? Title.GetHashCode() : 0);

        protected bool Equals(TestEntity other) => Title == other.Title;
    }
}