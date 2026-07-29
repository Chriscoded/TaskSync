using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TaskCompletedDomainEvent(Guid TaskId) : DomainEvent;