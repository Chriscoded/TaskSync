using MediatR;

namespace TaskSync.Application.Features.Comments.Commands.AddComment;

public sealed record AddCommentCommand(
    Guid TaskId,
    string Text)
    : IRequest<Guid>;