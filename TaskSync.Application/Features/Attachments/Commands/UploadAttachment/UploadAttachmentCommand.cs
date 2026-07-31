
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaskSync.Application.Features.Attachments.Commands.UploadAttachment;

public sealed record UploadAttachmentCommand(
    Guid TaskId,
    IFormFile File)
    : IRequest<Guid>;