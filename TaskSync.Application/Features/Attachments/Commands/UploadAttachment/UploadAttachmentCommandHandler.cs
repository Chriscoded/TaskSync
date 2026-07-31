using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;

namespace TaskSync.Application.Features.Attachments.Commands.UploadAttachment;

public sealed class UploadAttachmentCommandHandler
    : IRequestHandler<UploadAttachmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IFileStorageService _storage;

    public UploadAttachmentCommandHandler(
        IApplicationDbContext context,
        IFileStorageService storage)
    {
        _context = context;
        _storage = storage;
    }

    public async Task<Guid> Handle(
        UploadAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        await using var stream =
            request.File.OpenReadStream();

        var path =
            await _storage.UploadAsync(
                stream,
                request.File.FileName,
                cancellationToken);

        var attachment = Attachment.Create(
            request.TaskId,
            request.File.FileName,
            path);

        _context.Attachments.Add(attachment);

        await _context.SaveChangesAsync(cancellationToken);

        return attachment.Id;
    }
}