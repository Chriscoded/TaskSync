using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record UserDeactivatedDomainEvent(
    Guid UserId)
    : DomainEvent;