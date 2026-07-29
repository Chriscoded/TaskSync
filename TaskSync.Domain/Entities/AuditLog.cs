
using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class AuditLog : BaseAuditableEntity
{
    private AuditLog()
    {
    }

    public AuditLog(
        Guid tenantId,
        Guid userId,
        string action,
        string entityName,
        Guid entityId)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public Guid UserId { get; private set; }

    public string Action { get; private set; } = default!;

    public string EntityName { get; private set; } = default!;

    public Guid EntityId { get; private set; }
}