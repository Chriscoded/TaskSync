using Microsoft.AspNetCore.Mvc;
using TaskSync.Api.Controllers;
using TaskSync.Application.Features.Projects.Commands.CreateProject;
using TaskSync.Application.Features.Projects.Queries.GetProjects;

[Route("api/projects")]
public sealed class ProjectsController : BaseController
{
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
}