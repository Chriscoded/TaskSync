using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Repositories;

public sealed class ProjectRepository
    : Repository<Project>,
      IProjectRepository
{
    public ProjectRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<Project>> GetByTenantAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Projects
            .Where(x => x.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }
}