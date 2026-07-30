using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.DTOs;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetUsersQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(x => x.TenantId == _currentUser.TenantId)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email.Value,
                Status = x.Status.ToString()
            })
            .ToListAsync(cancellationToken);
    }
}