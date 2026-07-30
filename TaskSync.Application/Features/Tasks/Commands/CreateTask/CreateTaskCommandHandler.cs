using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskCommandHandler
    : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateTaskCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = TaskItem.Create(
            _currentUser.TenantId,
            request.ProjectId,
            new TaskTitle(request.Title),
            new TaskDescription(request.Description),
            (TaskPriority)request.Priority);

        _context.Tasks.Add(task);

        await _context.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}