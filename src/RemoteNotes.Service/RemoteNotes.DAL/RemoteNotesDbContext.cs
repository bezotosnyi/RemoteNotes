using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Core.Entities;

namespace RemoteNotes.DAL
{
    public class RemoteNotesDbContext : DbContext
    {
        public RemoteNotesDbContext(DbContextOptions<RemoteNotesDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}