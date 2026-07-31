using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(
        IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    private ClaimsPrincipal User =>
        _contextAccessor.HttpContext?.User
        ?? throw new UnauthorizedAccessException();

    public Guid UserId =>
        Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public Guid TenantId =>
        Guid.Parse(
            User.FindFirst("tenantId")!.Value);

    public string Email =>
        User.FindFirstValue(ClaimTypes.Email)!;
}