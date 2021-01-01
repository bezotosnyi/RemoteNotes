using Microsoft.EntityFrameworkCore;
namespace RemoteNotes.Tests.Fixture
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<TestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>(builder =>
            {
                builder.HasKey(pk => pk.Id);
                builder.Property(p => p.Id)
                    .HasColumnName("TestEntityId")
                    .ValueGeneratedOnAdd();

                builder.Property(p => p.Title)
                    .HasMaxLength(255)
                    .IsRequired();
            });
        }
    }
}