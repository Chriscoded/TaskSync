// ============================================
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200);

        builder.Property(x => x.Message)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.UserId);
    }
}