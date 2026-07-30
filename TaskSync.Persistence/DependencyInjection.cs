using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Persistence.Repositories;

namespace TaskSync.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<ITaskRepository, TaskRepository>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}