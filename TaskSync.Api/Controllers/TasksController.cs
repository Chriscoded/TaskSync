using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Tasks.Commands.CreateTask;
using TaskSync.Application.Features.Tasks.Queries.GetTasks;

namespace TaskSync.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/projects/{projectId:guid}/tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get(Guid projectId)
    {
        return Results.Ok(await _mediator.Send(new GetTasksQuery(projectId)));
    }

    [HttpPost]
    public async Task<IResult> Create(Guid projectId, CreateTaskRequest request)
    {
        var id = await _mediator.Send(new CreateTaskCommand(
            projectId,
            request.Title,
            request.Description,
            request.Priority));

        return Results.Created($"/api/tasks/{id}", id);
    }
}

public sealed record CreateTaskRequest(
    string Title,
    string Description,
    int Priority);