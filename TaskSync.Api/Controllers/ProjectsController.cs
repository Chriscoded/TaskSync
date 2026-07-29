using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Projects.Commands.CreateProject;
using TaskSync.Application.Features.Projects.Queries.GetProjects;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/projects")]
public sealed class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(
            await _mediator.Send(new GetProjectsQuery()));
    }

    [HttpPost]
    public async Task<IResult> Create(CreateProjectCommand command)
    {
        var id = await _mediator.Send(command);

        return Results.Created($"/api/projects/{id}", id);
    }
}