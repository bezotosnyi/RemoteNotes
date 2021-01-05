using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.Tests.Fixture
{
    public class TestDTO : BaseEntityDTO
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

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((TestEntity)obj);
        }

        public override int GetHashCode() => (Title != null ? Id.GetHashCode() ^ Title.GetHashCode() : 0);

        protected bool Equals(TestEntity other) => Id == other.Id && Title == other.Title;
    }
}