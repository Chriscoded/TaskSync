using MediatR;

namespace TaskSync.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(
    Guid ProjectId,
    string Title,
    string Description,
    int Priority) : IRequest<Guid>;