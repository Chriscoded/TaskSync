using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Auth.Login;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginCommand command)
    {
        var token = await _mediator.Send(command);

        return Results.Ok(new
        {
            accessToken = token
        });
    }
}