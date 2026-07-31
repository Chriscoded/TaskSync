using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Users.Queries.GetCurrentUser;

public sealed class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetCurrentUserQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<CurrentUserDto> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstAsync(
                x => x.Id == _currentUser.UserId,
                cancellationToken);

        return new CurrentUserDto(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName);
    }
}