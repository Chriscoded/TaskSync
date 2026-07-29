namespace TaskSync.Application.Interfaces;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(Guid userId);
}