using MediatR;

namespace TaskSync.Application.Features.Tasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(Guid Id)
    : IRequest;