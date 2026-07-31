using MediatR;

namespace TaskSync.Application.Features.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(Guid Id)
    : IRequest<ProjectDetailsDto>;

public sealed record ProjectDetailsDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAtUtc,
    int TotalTasks,
    int CompletedTasks);