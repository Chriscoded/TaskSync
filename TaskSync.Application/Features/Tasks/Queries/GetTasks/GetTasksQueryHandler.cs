using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.DTOs;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Tasks.Queries.GetTasks;

public sealed class GetTasksQueryHandler
    : IRequestHandler<GetTasksQuery, List<TaskDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTasksQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<TaskDto>> Handle(
        GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(x => x.ProjectId == request.ProjectId &&
                        x.TenantId == _currentUser.TenantId)
            .Select(x => new TaskDto
            {
                Id = x.Id,
                Title = x.Title.Value,
                Description = x.Description.Value,
                Status = x.Status.ToString(),
                Priority = x.Priority.ToString(),
                AssignedUserId = x.AssignedUserId
            })
            .ToListAsync(cancellationToken);
    }
}