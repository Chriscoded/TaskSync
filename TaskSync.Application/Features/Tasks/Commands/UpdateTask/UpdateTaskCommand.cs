using MediatR;

namespace TaskSync.Application.Features.Tasks.Commands.UpdateTask;

public sealed record UpdateTaskCommand(
    Guid Id,
    string Title,
    string Description,
    int Priority)
    : IRequest;