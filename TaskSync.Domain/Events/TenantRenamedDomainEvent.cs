using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record TenantRenamedDomainEvent(
    Guid TenantId,
    string PreviousName,
    string NewName)
    : DomainEvent;