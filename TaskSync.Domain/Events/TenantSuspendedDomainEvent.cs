using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TenantSuspendedDomainEvent(Guid TenantId)
    : DomainEvent;