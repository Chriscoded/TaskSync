namespace TaskSync.Domain.Interfaces;

public interface IUserRepository
{
    TaskSync.Domain.Entities.ApplicationUser? GetById(Guid id);
}