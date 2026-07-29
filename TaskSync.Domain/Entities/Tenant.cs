using TaskSync.Domain.Enums;
using TaskSync.Domain.ValueObjects;
using TaskSync.SharedKernel.Entities;
using TaskSync.Domain.Events;

namespace TaskSync.Domain.Entities;

public sealed class Tenant : AggregateRoot
{
    private Tenant()
    {
        // Required by EF Core
    }

    private Tenant(
        TenantName name,
        TenantSlug slug)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slug = slug;
        Status = TenantStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public TenantName Name { get; private set; } = default!;

    public TenantSlug Slug { get; private set; } = default!;

    public TenantStatus Status { get; private set; }

    public static Tenant Create(
        TenantName name,
        TenantSlug slug)
    {
        var tenant = new Tenant(name, slug);

        tenant.AddDomainEvent(
            new TenantCreatedDomainEvent(tenant.Id));

        return tenant;
    }

    public void Rename(TenantName newName)
    {
        ArgumentNullException.ThrowIfNull(newName);

        if (Name == newName)
            return;

        var previousName = Name.Value;

        Name = newName;

        AddDomainEvent(
            new TenantRenamedDomainEvent(
                Id,
                previousName,
                newName.Value));
    }

    public void ChangeSlug(TenantSlug newSlug)
    {
        ArgumentNullException.ThrowIfNull(newSlug);

        if (Slug == newSlug)
            return;

        var previousSlug = Slug.Value;

        Slug = newSlug;

        AddDomainEvent(
            new TenantSlugChangedDomainEvent(
                Id,
                previousSlug,
                newSlug.Value));
    }

    public void Suspend()
    {
        if (Status == TenantStatus.Suspended)
            return;

        Status = TenantStatus.Suspended;

        AddDomainEvent(
            new TenantSuspendedDomainEvent(Id));
    }

    public void Activate()
    {
        if (Status == TenantStatus.Active)
            return;

        Status = TenantStatus.Active;

        AddDomainEvent(
            new TenantActivatedDomainEvent(Id));
    }
}