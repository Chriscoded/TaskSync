using TaskSync.SharedKernel.Events;

namespace TaskSync.Domain.Events;

public sealed record UserEmailChangedDomainEvent(
    Guid UserId,
    string PreviousEmail,
    string NewEmail)
    : DomainEvent;