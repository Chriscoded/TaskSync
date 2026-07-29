using Microsoft.Extensions.DependencyInjection;
using TaskSync.Application.Interfaces;
using TaskSync.Infrastructure.Services;

namespace TaskSync.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}