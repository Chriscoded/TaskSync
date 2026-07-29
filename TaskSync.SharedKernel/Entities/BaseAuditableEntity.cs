namespace TaskSync.SharedKernel.Entities;

public abstract class BaseAuditableEntity : Entity<Guid>
{
    public DateTime CreatedAtUtc { get; protected set; }

    public Guid? CreatedBy { get; protected set; }

    public DateTime? LastModifiedAtUtc { get; protected set; }

    public Guid? LastModifiedBy { get; protected set; }
}