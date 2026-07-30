using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Notifications.Queries.GetNotifications;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public sealed class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(
            await _mediator.Send(new GetNotificationsQuery()));
    }
}