using MediatR;
using TaskSync.Application.DTOs;

namespace TaskSync.Application.Features.Users.Queries.GetUsers;

public sealed record GetUsersQuery
    : IRequest<List<UserDto>>;