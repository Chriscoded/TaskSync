using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskSync.Application.Features.Attachments.Commands.CreateAttachment;

namespace TaskSync.Api.Controllers;

[ApiController]
[Route("api/tasks/{taskId:guid}/attachments")]
public sealed class AttachmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttachmentsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IResult> Upload(
        Guid taskId,
        UploadAttachmentRequest request)
    {
        var id = await _mediator.Send(
            new CreateAttachmentCommand(
                taskId,
                request.FileName,
                request.Path));

        return Results.Created(
            $"/api/attachments/{id}",
            id);
    }
}

public sealed record UploadAttachmentRequest(
    string FileName,
    string Path);