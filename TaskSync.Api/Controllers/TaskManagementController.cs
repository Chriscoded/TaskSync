using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Tasks.Commands.AssignTask;
using TaskSync.Application.Features.Tasks.Commands.DeleteTask;
using TaskSync.Application.Features.Tasks.Commands.UpdateTask;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public sealed class TaskManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> Update(
        Guid id,
        UpdateTaskRequest request)
    {
        await _mediator.Send(new UpdateTaskCommand(
            id,
            request.Title,
            request.Description,
            request.Priority));

        return Results.NoContent();
    }

    [HttpPut("{id:guid}/assign")]
    public async Task<IResult> Assign(
        Guid id,
        AssignTaskRequest request)
    {
        await _mediator.Send(
            new AssignTaskCommand(id, request.UserId));

        return Results.NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTaskCommand(id));

        return Results.NoContent();
    }
}

public sealed record UpdateTaskRequest(
    string Title,
    string Description,
    int Priority);

public sealed record AssignTaskRequest(
    Guid UserId);