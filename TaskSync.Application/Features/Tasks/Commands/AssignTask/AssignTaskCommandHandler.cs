using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Tasks.Commands.AssignTask;

public sealed class AssignTaskCommandHandler
    : IRequestHandler<AssignTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public AssignTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        AssignTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstAsync(x => x.Id == request.TaskId, cancellationToken);

        task.Assign(request.UserId);

        await _context.SaveChangesAsync(cancellationToken);
    }
}