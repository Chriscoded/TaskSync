using TaskSync.Domain.Entities;

namespace TaskSync.Application.Interfaces;

public interface ITaskRepository
    : IRepository<TaskItem>
{
    Task<List<TaskItem>> GetProjectTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);
}