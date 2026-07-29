using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class Attachment : BaseAuditableEntity
{
    private Attachment()
    {
    }

    public Attachment(
        Guid tenantId,
        Guid taskItemId,
        string fileName,
        string path)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        TaskItemId = taskItemId;
        FileName = fileName;
        Path = path;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public Guid TaskItemId { get; private set; }

    public string FileName { get; private set; } = default!;

    public string Path { get; private set; } = default!;
}