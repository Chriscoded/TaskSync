namespace TaskSync.Application.DTOs;

public sealed class UserDto
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string Status { get; init; } = default!;
}