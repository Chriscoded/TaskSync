using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Tenants.Queries.GetTenants;

public sealed class GetTenantsHandler
    : IRequestHandler<GetTenantsQuery, List<TenantDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTenantsHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TenantDto>> Handle(
        GetTenantsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Tenants
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new TenantDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Slug = x.Slug.Value
            })
            .ToListAsync(cancellationToken);
    }
}