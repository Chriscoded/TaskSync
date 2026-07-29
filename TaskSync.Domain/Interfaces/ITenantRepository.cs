namespace TaskSync.Domain.Interfaces;

public interface ITenantRepository
{
    TaskSync.Domain.Entities.Tenant? GetById(Guid id);
}