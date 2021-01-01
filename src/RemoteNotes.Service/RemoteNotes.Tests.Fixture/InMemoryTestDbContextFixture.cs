using System;
using Microsoft.EntityFrameworkCore;

namespace RemoteNotes.Tests.Fixture
{
    public class InMemoryTestDbContextFixture : IDisposable
    {
        private readonly string _databaseName = Guid.NewGuid().ToString();

        public TestDbContext DbContext { get; }

        public InMemoryTestDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;
            DbContext = new TestDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext?.Dispose();
        }
    }
}