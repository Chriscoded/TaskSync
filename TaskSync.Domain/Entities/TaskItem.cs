
using TaskSync.Domain.Enums;
using TaskSync.Domain.Events;
using TaskSync.Domain.ValueObjects;
using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class TaskItem : AggregateRoot
{
    private TaskItem()
    {
    }

    private TaskItem(
        Guid tenantId,
        Guid projectId,
        TaskTitle title,
        TaskDescription description,
        TaskPriority priority)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Priority = priority;
        Status = TasksStatus.Todo;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public Guid ProjectId { get; private set; }

    public Guid? AssignedUserId { get; private set; }

    public TaskTitle Title { get; private set; } = default!;

    public TaskDescription Description { get; private set; } = default!;

    public TaskPriority Priority { get; private set; }

    public TasksStatus Status { get; private set; }

    public DateTime? DueDate { get; private set; }

    public static TaskItem Create(
        Guid tenantId,
        Guid projectId,
        TaskTitle title,
        TaskDescription description,
        TaskPriority priority)
    {
        var task = new TaskItem(
            tenantId,
            projectId,
            title,
            description,
            priority);

        task.AddDomainEvent(
            new TaskCreatedDomainEvent(task.Id));

        return task;
    }

    public void Assign(Guid userId)
    {
        AssignedUserId = userId;

        AddDomainEvent(
            new TaskAssignedDomainEvent(Id, userId));
    }

    public void ChangeTitle(TaskTitle title)
    {
        Title = title;
    }

    public void ChangeDescription(TaskDescription description)
    {
        Description = description;
    }

    public void ChangePriority(TaskPriority priority)
    {
        Priority = priority;
    }

    public void SetDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
    }

    public void Start()
    {
        Status = TasksStatus.InProgress;
    }

    public void Complete()
    {
        Status = TasksStatus.Completed;

        AddDomainEvent(
            new TaskCompletedDomainEvent(Id));
    }

    public void Cancel()
    {
        Status = TasksStatus.Cancelled;
    }
}