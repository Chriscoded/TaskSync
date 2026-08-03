using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Tenants.Commands.CreateTenant;
using TaskSync.Application.Tenants.Queries.GetTenants;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TenantController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetTenantsQuery());

        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<IResult> Create(
        CreateTenantCommand command)
    {
        var id = await _mediator.Send(command);

        return Results.Ok(id);
    }
}