using MediatR;

namespace TaskSync.Application.Features.Attachments.Commands.CreateAttachment;

public sealed record CreateAttachmentCommand(
    Guid TaskId,
    string FileName,
    string Path)
    : IRequest<Guid>;