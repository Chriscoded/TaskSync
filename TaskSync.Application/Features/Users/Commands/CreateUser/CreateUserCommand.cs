using MediatR;

namespace TaskSync.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email)
    : IRequest<Guid>;