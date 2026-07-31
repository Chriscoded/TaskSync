using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Auth.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException();

        if (!_passwordHasher.Verify(
            user.PasswordHash,
            request.Password))
        {
            throw new UnauthorizedAccessException();
        }

        return await _jwtService.GenerateTokenAsync(user.Id);
    }
}