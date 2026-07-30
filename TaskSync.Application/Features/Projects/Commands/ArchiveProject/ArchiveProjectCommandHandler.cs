using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Projects.Commands.ArchiveProject;

public sealed class ArchiveProjectCommandHandler
    : IRequestHandler<ArchiveProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public ArchiveProjectCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        ArchiveProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstAsync(x => x.Id == request.Id, cancellationToken);

        project.Archive();

        await _context.SaveChangesAsync(cancellationToken);
    }
}