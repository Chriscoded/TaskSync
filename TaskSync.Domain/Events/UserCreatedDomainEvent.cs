using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record UserCreatedDomainEvent(
    Guid UserId,
    Guid TenantId)
    : DomainEvent;