using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Enums;

namespace TaskSync.Application.Features.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdQueryHandler
    : IRequestHandler<GetProjectByIdQuery, ProjectDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetProjectByIdQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<ProjectDetailsDto> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Include(x => x.Tasks)
            .FirstAsync(
                x => x.Id == request.Id &&
                     x.TenantId == _currentUser.TenantId,
                cancellationToken);

        return new ProjectDetailsDto(
            project.Id,
            project.Name,
            project.Description,
            project.CreatedAtUtc,
            project.Tasks.Count,
            project.Tasks.Count(x => x.Status == TasksStatus.Completed));
    }
}