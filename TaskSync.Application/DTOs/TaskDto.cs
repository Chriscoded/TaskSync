namespace TaskSync.Application.DTOs;

public sealed class TaskDto
{
    public Guid Id { get; init; }

    public string Title { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string Status { get; init; } = default!;

    public string Priority { get; init; } = default!;

    public Guid? AssignedUserId { get; init; }
}