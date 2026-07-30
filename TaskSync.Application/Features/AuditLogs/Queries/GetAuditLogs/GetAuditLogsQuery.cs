using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Features.AuditLogs.Queries.GetAuditLogs;

public sealed record GetAuditLogsQuery()
    : IRequest<List<AuditLogDto>>;

public sealed record AuditLogDto(
    Guid Id,
    string Action,
    string EntityName,
    Guid EntityId,
    DateTime CreatedAtUtc);

public sealed class GetAuditLogsQueryHandler
    : IRequestHandler<GetAuditLogsQuery, List<AuditLogDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAuditLogsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<AuditLogDto>> Handle(
        GetAuditLogsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.AuditLogs
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new AuditLogDto(
                x.Id,
                x.Action,
                x.EntityName,
                x.EntityId,
                x.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}