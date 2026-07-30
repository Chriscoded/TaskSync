// ============================================
// Api/Controllers/UsersController.cs
// ============================================

using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Users.Commands.CreateUser;
using TaskSync.Application.Features.Users.Queries.GetUsers;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(await _mediator.Send(new GetUsersQuery()));
    }

    [HttpPost]
    public async Task<IResult> Create(CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        return Results.Created($"/api/users/{id}", id);
    }
}