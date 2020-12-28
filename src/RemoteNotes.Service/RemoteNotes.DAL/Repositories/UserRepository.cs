using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Core.Entities;

namespace RemoteNotes.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}