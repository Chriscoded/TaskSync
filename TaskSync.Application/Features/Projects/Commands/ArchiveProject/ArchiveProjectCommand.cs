using MediatR;

namespace TaskSync.Application.Features.Projects.Commands.ArchiveProject;

public sealed record ArchiveProjectCommand(Guid Id)
    : IRequest;