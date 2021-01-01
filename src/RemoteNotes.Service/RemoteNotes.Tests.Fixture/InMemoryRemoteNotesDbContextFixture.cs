using System;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL;

namespace RemoteNotes.Tests.Fixture
{
    public class InMemoryRemoteNotesDbContextFixture : IDisposable
    {
        private readonly string _databaseName = Guid.NewGuid().ToString();

        public RemoteNotesDbContext DbContext { get; }

        public InMemoryRemoteNotesDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<RemoteNotesDbContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;
            DbContext = new RemoteNotesDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}