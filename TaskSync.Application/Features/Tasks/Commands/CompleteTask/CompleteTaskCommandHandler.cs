using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Tasks.Commands.CompleteTask;

public sealed class CompleteTaskCommandHandler
    : IRequestHandler<CompleteTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public CompleteTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        CompleteTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstAsync(x => x.Id == request.TaskId, cancellationToken);

        task.Complete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}