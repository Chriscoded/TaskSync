using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Api.Controllers;
using TaskSync.Application.Features.Projects.Commands.CreateProject;
using TaskSync.Application.Features.Projects.Commands.DeleteProject;
using TaskSync.Application.Features.Projects.Queries.GetProjectById;
using TaskSync.Application.Features.Projects.Queries.GetProjects;

[Authorize]
[ApiController]
[Route("api/projects")]
public sealed class ProjectsController : BaseController
{
    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(
            await Sender.Send(new GetProjectsQuery()));
    }

    [HttpPost]
    public async Task<IResult> Create(
        CreateProjectCommand command)
    {
        var id = await Sender.Send(command);

        return Results.Created(
            $"/api/projects/{id}",
            id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> Get(Guid id)
    {
        return Results.Ok(
            await _mediator.Send(
                new GetProjectByIdQuery(id)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));

        return Results.NoContent();
    }
}