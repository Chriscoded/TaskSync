using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record ProjectArchivedDomainEvent(Guid ProjectId) : DomainEvent;