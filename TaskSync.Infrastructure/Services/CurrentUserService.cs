using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId =>
        Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public Guid TenantId =>
        Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue("tenantId")!);

    public string Email =>
        _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;
}