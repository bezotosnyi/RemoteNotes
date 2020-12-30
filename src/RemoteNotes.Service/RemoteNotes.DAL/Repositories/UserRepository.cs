using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}