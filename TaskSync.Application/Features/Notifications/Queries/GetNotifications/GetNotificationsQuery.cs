using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Notifications.Queries.GetNotifications;

public sealed record GetNotificationsQuery()
    : IRequest<List<NotificationDto>>;

public sealed record NotificationDto(
    Guid Id,
    string Title,
    string Message,
    bool IsRead);

public sealed class GetNotificationsQueryHandler
    : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetNotificationsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<NotificationDto>> Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(x => x.UserId == _currentUser.UserId)
            .Select(x => new NotificationDto(
                x.Id,
                x.Title,
                x.Message,
                x.IsRead))
            .ToListAsync(cancellationToken);
    }
}