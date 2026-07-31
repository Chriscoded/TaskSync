using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Projects.Commands.DeleteProject;

public sealed class DeleteProjectCommandHandler
    : IRequestHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (project is null)
            throw new Exception("Project not found.");

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync(cancellationToken);
    }
}