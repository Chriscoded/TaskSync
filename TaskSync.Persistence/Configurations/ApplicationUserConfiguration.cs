using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.Email
        }).IsUnique();

        builder.Property(x => x.PasswordHash)
        .HasMaxLength(500)
        .IsRequired();
    }
}