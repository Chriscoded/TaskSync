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
        var entity = await _context.Tasks
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        _context.Tasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}