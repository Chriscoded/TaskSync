using MediatR;
using TaskSync.Application.DTOs;

namespace TaskSync.Application.Features.Tasks.Queries.GetTasks;

public sealed record GetTasksQuery(Guid ProjectId)
    : IRequest<List<TaskDto>>;