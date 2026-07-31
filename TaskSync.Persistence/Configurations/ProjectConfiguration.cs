using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                x => x.Value,
                x => new(x))
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(
                x => x.Value,
                x => new(x))
            .HasMaxLength(1000);

        builder.Property(x => x.Status);

        builder.Property(x => x.TenantId);

        builder.HasIndex(x => x.TenantId);

        builder.HasMany(x => x.Tasks)
           .WithOne(x => x.Project)
           .HasForeignKey(x => x.ProjectId)
           .OnDelete(DeleteBehavior.Cascade);

    }
}