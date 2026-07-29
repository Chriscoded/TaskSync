using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TenantActivatedDomainEvent(Guid TenantId)
    : DomainEvent;