using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record UserActivatedDomainEvent(
    Guid UserId)
    : DomainEvent;