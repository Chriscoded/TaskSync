using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record ProjectCreatedDomainEvent(Guid ProjectId) : DomainEvent;