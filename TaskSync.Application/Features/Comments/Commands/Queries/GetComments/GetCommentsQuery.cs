// ============================================
// Application/Features/Comments/Queries/GetComments/GetCommentsQuery.cs
// ============================================

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.Comments.Queries.GetComments;

public sealed record GetCommentsQuery(Guid TaskId)
    : IRequest<List<CommentDto>>;

public sealed record CommentDto(
    Guid Id,
    string Text,
    Guid UserId,
    DateTime CreatedAtUtc);

public sealed class GetCommentsQueryHandler
    : IRequestHandler<GetCommentsQuery, List<CommentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCommentsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentDto>> Handle(
        GetCommentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Comments
            .Where(x => x.TaskItemId == request.TaskId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new CommentDto(
                x.Id,
                x.Text,
                x.UserId,
                x.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}