using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id)
                .HasColumnName("UserId")
                .ValueGeneratedOnAdd();

            builder.HasOne(o => o.Account)
                .WithOne(o => o.User)
                .HasForeignKey<Account>(fk => fk.UserId)
                .IsRequired();

            builder.Property(p => p.Login)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasDefaultValueSql("1")
                .IsRequired();
        }
    }
}