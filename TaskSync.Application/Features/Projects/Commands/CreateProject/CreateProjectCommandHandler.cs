using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Application.Features.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateProjectCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = Project.Create(
            _currentUser.TenantId,
            new ProjectName(request.Name),
            new ProjectDescription(request.Description));

        _context.Projects.Add(project);

        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}