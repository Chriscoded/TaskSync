using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskSync.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private ISender? _sender;

    protected ISender Sender =>
        _sender ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();
}