namespace TaskSync.Domain.Interfaces;

public interface ITaskRepository
{
    TaskSync.Domain.Entities.TaskItem? GetById(Guid id);
}