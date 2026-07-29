using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TaskCreatedDomainEvent(Guid TaskId) : DomainEvent;