using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.TaskItemId);
    }
}