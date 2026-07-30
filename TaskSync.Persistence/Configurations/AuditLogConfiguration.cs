using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .HasMaxLength(100);

        builder.Property(x => x.EntityName)
            .HasMaxLength(100);

        builder.HasIndex(x => x.EntityId);
    }
}