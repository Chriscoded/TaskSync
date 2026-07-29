using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                x => x.Value,
                x => new(x));

        builder.Property(x => x.Slug)
            .HasConversion(
                x => x.Value,
                x => new(x));

        builder.HasIndex(x => x.Slug)
            .IsUnique();
    }
}