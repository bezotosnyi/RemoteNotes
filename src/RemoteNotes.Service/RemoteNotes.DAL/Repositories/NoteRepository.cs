using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Core.Entities;

namespace RemoteNotes.DAL.Repositories
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(DbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}