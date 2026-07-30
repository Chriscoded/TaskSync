using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;

namespace TaskSync.Application.Features.Auth.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Email.Value == request.Email.ToLower(),
            cancellationToken);

        if (user is null)
            throw new Exception("Invalid credentials.");

        return await _jwtService.GenerateTokenAsync(user.Id);
    }
}