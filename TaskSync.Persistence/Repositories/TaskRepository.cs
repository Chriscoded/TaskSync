// ============================================
// Persistence/Repositories/TaskRepository.cs
// ============================================

using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;

namespace TaskSync.Persistence.Repositories;

public sealed class TaskRepository
    : Repository<TaskItem>,
      ITaskRepository
{
    public TaskRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<List<TaskItem>> GetProjectTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Tasks
            .Where(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }
}