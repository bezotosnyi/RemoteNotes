using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Core.Entities;

namespace RemoteNotes.DAL.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}