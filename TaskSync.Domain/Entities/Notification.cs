using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class Notification : AggregateRoot
{
    private Notification()
    {
    }

    public Notification(
        Guid tenantId,
        Guid userId,
        string title,
        string message)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        UserId = userId;
        Title = title;
        Message = message;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public Guid UserId { get; private set; }

    public string Title { get; private set; } = default!;

    public string Message { get; private set; } = default!;

    public bool IsRead { get; private set; }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}