using RemoteNotes.Tests.Fixture;
using Xunit;

namespace RemoteNotes.DAL.Tests.Integration
{
    public class RemoteNotesDbContextTests : IClassFixture<InMemoryRemoteNotesDbContextFixture>
    {
        private readonly RemoteNotesDbContext _sut;

        public RemoteNotesDbContextTests(InMemoryRemoteNotesDbContextFixture inMemoryRemoteNotesDbContextFixture)
        {
            _sut = inMemoryRemoteNotesDbContextFixture.DbContext;
        }

        [Fact]
        public void EnsureCreated_IfCreateDbContext_EnsuresCreatedSuccess()
        {
            // act
            var dbExist = _sut.Database.CanConnect();

            // assert
            Assert.True(dbExist);
        }

        [Fact]
        public void DbSet_IfCreateDbContext_DbSetsIsNotNull()
        {
            // assert
            Assert.NotNull(_sut.Accounts);
            Assert.NotNull(_sut.Notes);
            Assert.NotNull(_sut.Users);
        }
    }
}