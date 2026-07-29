using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.DTOs;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Projects.Queries.GetProjects;

public sealed class GetProjectsQueryHandler
    : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetProjectsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<ProjectDto>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(x => x.TenantId == _currentUser.TenantId)
            .Select(x => new ProjectDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Description = x.Description.Value,
                Status = x.Status.ToString()
            })
            .ToListAsync(cancellationToken);
    }
}