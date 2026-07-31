// ============================================
// Application/Features/Auth/Register/RegisterCommand.cs
// ============================================

using MediatR;

namespace TaskSync.Application.Features.Auth.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid TenantId)
    : IRequest<Guid>;