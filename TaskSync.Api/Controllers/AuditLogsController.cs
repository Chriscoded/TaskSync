using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.AuditLogs.Queries.GetAuditLogs;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/auditlogs")]
public sealed class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(
            await _mediator.Send(new GetAuditLogsQuery()));
    }
}