using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TaskAssignedDomainEvent(
    Guid TaskId,
    Guid UserId) : DomainEvent;