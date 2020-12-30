using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.DAL.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id)
                .HasColumnName("NoteId")
                .ValueGeneratedOnAdd();

            builder.HasOne(o => o.Account)
                .WithMany()
                .HasForeignKey(fk => fk.UserId)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Text)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Image);

            builder.Property(p => p.PublishTime).IsRequired();
            builder.Property(p => p.ModifyTime);

            builder.HasIndex(i => new { i.Id, i.UserId });
        }
    }
}