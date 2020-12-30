using System;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL;

namespace RemoteNotes.Tests.Fixture
{
    public class InMemoryDatabaseFixture : IDisposable
    {
        private readonly string DatabaseName = Guid.NewGuid().ToString();

        public RemoteNotesDbContext DbContext { get; }

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<RemoteNotesDbContext>()
                .UseInMemoryDatabase(databaseName: DatabaseName)
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