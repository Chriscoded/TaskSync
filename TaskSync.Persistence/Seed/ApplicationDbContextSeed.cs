using TaskSync.Domain.Entities;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Persistence.Seed;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Tenants.Any())
            return;

        var tenant = Tenant.Create(
            new TenantName("Demo Company"),
            new TenantSlug("demo-company"));

        context.Tenants.Add(tenant);

        var user = ApplicationUser.Create(
            tenant.Id,
            "Admin",
            "User",
            new Email("admin@tasksync.com"));

        context.Users.Add(user);

        var project = Project.Create(
            tenant.Id,
            new ProjectName("Demo Project"),
            new ProjectDescription("Initial seeded project"));

        context.Projects.Add(project);

        var task = TaskItem.Create(
            tenant.Id,
            project.Id,
            new TaskTitle("Welcome Task"),
            new TaskDescription("Complete your first task."),
            Domain.Enums.TaskPriority.High);

        context.Tasks.Add(task);

        await context.SaveChangesAsync();
    }
}