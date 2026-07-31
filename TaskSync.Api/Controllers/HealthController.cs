using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Health;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    private readonly IMediator _mediator;

    public HealthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(await _mediator.Send(new HealthQuery()));
    }
}