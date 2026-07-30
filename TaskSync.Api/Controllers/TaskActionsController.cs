using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Tasks.Commands.CompleteTask;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public sealed class TaskActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IResult> Complete(Guid id)
    {
        await _mediator.Send(new CompleteTaskCommand(id));

        return Results.NoContent();
    }
}