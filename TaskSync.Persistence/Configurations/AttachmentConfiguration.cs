using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName)
            .HasMaxLength(255);

        builder.Property(x => x.Path)
            .HasMaxLength(1000);

        builder.HasIndex(x => x.TaskItemId);
    }
}