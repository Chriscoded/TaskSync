using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Projects.Commands.ArchiveProject;
using TaskSync.Application.Features.Projects.Commands.DeleteProject;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/projects")]
public sealed class ProjectActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));

        return Results.NoContent();
    }

    [HttpPut("{id:guid}/archive")]
    public async Task<IResult> Archive(Guid id)
    {
        await _mediator.Send(new ArchiveProjectCommand(id));

        return Results.NoContent();
    }
}