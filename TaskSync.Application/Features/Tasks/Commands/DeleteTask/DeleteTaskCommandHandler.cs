using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Tasks.Commands.DeleteTask;

public sealed class DeleteTaskCommandHandler
    : IRequestHandler<DeleteTaskCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (task is null)
            throw new Exception("Task not found.");

        _context.Tasks.Remove(task);

        await _context.SaveChangesAsync(cancellationToken);
    }
}