using MediatR;

namespace TaskSync.Application.Features.Tasks.Queries.GetTaskById;

public sealed record GetTaskByIdQuery(Guid Id)
    : IRequest<TaskDetailsDto>;

public sealed record TaskDetailsDto(
    Guid Id,
    string Title,
    string Description,
    string Status,
    string Priority,
    Guid? AssignedUserId);