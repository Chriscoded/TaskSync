using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;
using TaskSync.Domain.Enums;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        task.ChangeTitle(new TaskTitle(request.Title));
        task.ChangeDescription(new TaskDescription(request.Description));
        task.ChangePriority((TaskPriority)request.Priority);

        await _context.SaveChangesAsync(cancellationToken);
    }
}