using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Repositories;

namespace RemoteNotes.Tests.Fixture
{
    public class TestRepository : RepositoryBase<TestEntity>
    {
        public TestRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}