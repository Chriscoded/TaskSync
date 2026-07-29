using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasConversion(
                x => x.Value,
                x => new(x))
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasConversion(
                x => x.Value,
                x => new(x))
            .HasMaxLength(2000);

        builder.Property(x => x.Priority);

        builder.Property(x => x.Status);

        builder.HasIndex(x => x.ProjectId);

        builder.HasIndex(x => x.TenantId);
    }
}