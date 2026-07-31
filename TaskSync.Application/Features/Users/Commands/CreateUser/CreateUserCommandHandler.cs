using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateUserCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = ApplicationUser.Create(
            _currentUser.TenantId,
            request.FirstName,
            request.LastName,
            new string(request.Email));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}