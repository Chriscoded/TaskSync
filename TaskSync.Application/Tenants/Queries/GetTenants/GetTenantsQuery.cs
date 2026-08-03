using MediatR;

namespace TaskSync.Application.Tenants.Queries.GetTenants;

public sealed record GetTenantsQuery()
    : IRequest<List<TenantDto>>;