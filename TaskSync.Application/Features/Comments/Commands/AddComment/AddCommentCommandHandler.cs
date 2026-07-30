using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;

namespace TaskSync.Application.Features.Comments.Commands.AddComment;

public sealed class AddCommentCommandHandler
    : IRequestHandler<AddCommentCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public AddCommentCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        AddCommentCommand request,
        CancellationToken cancellationToken)
    {
        var comment = Comment.Create(
            request.TaskId,
            _currentUser.UserId,
            request.Text);

        _context.Comments.Add(comment);

        await _context.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}