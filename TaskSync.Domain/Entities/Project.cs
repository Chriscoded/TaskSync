using TaskSync.Domain.Enums;
using TaskSync.Domain.Events;
using TaskSync.Domain.ValueObjects;
using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class Project : AggregateRoot
{
    private readonly List<TaskItem> _tasks = [];

    private Project()
    {
    }

    private Project(
        Guid tenantId,
        ProjectName name,
        ProjectDescription description)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        Name = name;
        Description = description;
        Status = ProjectStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public ProjectName Name { get; private set; } = default!;

    public ProjectDescription Description { get; private set; } = default!;

    public ProjectStatus Status { get; private set; }

    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    public static Project Create(
        Guid tenantId,
        ProjectName name,
        ProjectDescription description)
    {
        var project = new Project(
            tenantId,
            name,
            description);

        project.AddDomainEvent(
            new ProjectCreatedDomainEvent(project.Id));

        return project;
    }

    public void AddTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        _tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);

        _tasks.Remove(task);
    }

    public void Rename(ProjectName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (Name == name)
            return;

        Name = name;
    }

    public void ChangeDescription(ProjectDescription description)
    {
        ArgumentNullException.ThrowIfNull(description);

        Description = description;
    }

    public void Archive()
    {
        if (Status == ProjectStatus.Archived)
            return;

        Status = ProjectStatus.Archived;

        AddDomainEvent(
            new ProjectArchivedDomainEvent(Id));
    }

    public void Reopen()
    {
        Status = ProjectStatus.Active;
    }
}