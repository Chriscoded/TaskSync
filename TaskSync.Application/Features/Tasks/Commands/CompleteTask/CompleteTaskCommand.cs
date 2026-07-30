using MediatR;

namespace TaskSync.Application.Features.Tasks.Commands.CompleteTask;

public sealed record CompleteTaskCommand(Guid TaskId)
    : IRequest;