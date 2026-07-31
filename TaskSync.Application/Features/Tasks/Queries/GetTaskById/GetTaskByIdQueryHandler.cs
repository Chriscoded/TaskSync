using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Tasks.Queries.GetTaskById;

public sealed class GetTaskByIdQueryHandler
    : IRequestHandler<GetTaskByIdQuery, TaskDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetTaskByIdQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<TaskDetailsDto> Handle(
        GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstAsync(
                x => x.Id == request.Id &&
                     x.TenantId == _currentUser.TenantId,
                cancellationToken);

        return new TaskDetailsDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(),
            task.Priority.ToString(),
            task.AssignedUserId);
    }
}