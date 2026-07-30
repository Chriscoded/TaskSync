using MediatR;

namespace TaskSync.Application.Features.Tasks.Commands.AssignTask;

public sealed record AssignTaskCommand(
    Guid TaskId,
    Guid UserId) : IRequest;