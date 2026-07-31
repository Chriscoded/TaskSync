using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Attachments.Commands.UploadAttachment;

namespace TaskSync.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks/{taskId:guid}/attachments")]
public sealed class AttachmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttachmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IResult> Upload(
        Guid taskId,
        IFormFile file)
    {
        var id = await _mediator.Send(
            new UploadAttachmentCommand(
                taskId,
                file));

        return Results.Created(
            $"/api/attachments/{id}",
            id);
    }
}