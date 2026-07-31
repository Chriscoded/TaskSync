using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Dashboard.Queries.GetDashboard;

namespace TaskSync.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(
            await _mediator.Send(
                new GetDashboardQuery()));
    }
}