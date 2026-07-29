using TaskSync.SharedKernel.Events;

public sealed record TenantSlugChangedDomainEvent(
    Guid TenantId,
    string PreviousSlug,
    string NewSlug)
    : DomainEvent;