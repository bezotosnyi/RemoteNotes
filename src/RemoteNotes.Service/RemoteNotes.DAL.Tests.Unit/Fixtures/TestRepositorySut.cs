using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.DAL.Repositories;

namespace RemoteNotes.DAL.Tests.Unit.Fixtures
{
    // since the RepositoryBase class is abstract – creates new TestRepositorySut
    // SUT - subject under test
    public class TestRepositorySut : RepositoryBase<BaseEntity>
    {
        public TestRepositorySut(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}