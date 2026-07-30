
using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class Comment : BaseAuditableEntity
{
    private Comment()
    {
    }

    public Comment(
        Guid tenantId,
        Guid taskId,
        Guid userId,
        string text)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        TaskItemId = taskId;
        UserId = userId;
        Text = text.Trim();

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public Guid TaskItemId { get; private set; }

    public Guid UserId { get; private set; }

    public string Text { get; private set; } = default!;

    public static Comment Create(
    Guid taskItemId,
    Guid userId,
    string text)
    {
        return new Comment
        {
            Id = Guid.NewGuid(),
            TaskItemId = taskItemId,
            UserId = userId,
            Text = text
        };
    }
}