using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Dashboard.Queries.GetDashboard;

public sealed record GetDashboardQuery()
    : IRequest<DashboardDto>;

public sealed record DashboardDto(
    int Projects,
    int Tasks,
    int CompletedTasks,
    int Users);

public sealed class GetDashboardQueryHandler
    : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetDashboardQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<DashboardDto> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken)
    {
        var tenantId = _currentUser.TenantId;

        return new DashboardDto(
            await _context.Projects.CountAsync(
                x => x.TenantId == tenantId,
                cancellationToken),

            await _context.Tasks.CountAsync(
                x => x.TenantId == tenantId,
                cancellationToken),

            await _context.Tasks.CountAsync(
                x => x.TenantId == tenantId &&
                     x.Status == Domain.Enums.TasksStatus.Completed,
                cancellationToken),

            await _context.Users.CountAsync(
                x => x.TenantId == tenantId,
                cancellationToken));
    }
}