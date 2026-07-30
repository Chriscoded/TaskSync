// ============================================
// Application/Features/Auth/Login/LoginCommand.cs
// ============================================

using MediatR;

namespace TaskSync.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : IRequest<string>;