using TaskSync.Domain.Entities;

namespace TaskSync.Application.Interfaces;

public interface IProjectRepository
    : IRepository<Project>
{
    Task<List<Project>> GetByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default);
}