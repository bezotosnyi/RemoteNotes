using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.DAL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Ignore(p => p.Id);
            builder.HasKey(pk => pk.UserId);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Nickname)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Birthday).IsRequired();
            builder.Property(p => p.Photo);

            builder.Property(p => p.CreateTime).IsRequired();
            builder.Property(p => p.ModifyTime);
        }
    }
}