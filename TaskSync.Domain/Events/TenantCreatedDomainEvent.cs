using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TenantCreatedDomainEvent(Guid TenantId)
    : DomainEvent;