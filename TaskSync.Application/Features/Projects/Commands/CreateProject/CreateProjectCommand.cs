using MediatR;

namespace TaskSync.Application.Features.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(
    string Name,
    string Description) : IRequest<Guid>;