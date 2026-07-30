
using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Domain.Entities;

namespace TaskSync.Application.Features.Attachments.Commands.CreateAttachment;

public sealed class CreateAttachmentCommandHandler
    : IRequestHandler<CreateAttachmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAttachmentCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateAttachmentCommand request,
        CancellationToken cancellationToken)
    {
        var attachment = Attachment.Create(
            request.TaskId,
            request.FileName,
            request.Path);

        _context.Attachments.Add(attachment);

        await _context.SaveChangesAsync(cancellationToken);

        return attachment.Id;
    }
}