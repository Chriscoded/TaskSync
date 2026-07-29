namespace TaskSync.Domain.Interfaces;

public interface IProjectRepository
{
    TaskSync.Domain.Entities.Project? GetById(Guid id);
}