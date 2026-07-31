using MediatR;

namespace TaskSync.Application.Features.Users.Queries.GetCurrentUser;

public sealed record GetCurrentUserQuery()
    : IRequest<CurrentUserDto>;

public sealed record CurrentUserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);