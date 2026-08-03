// ============================================
// Application/Tenants/Commands/CreateTenant/CreateTenantHandler.cs
// ============================================

using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Domain.Entities;
using TaskSync.Domain.ValueObjects;

namespace TaskSync.Application.Tenants.Commands.CreateTenant;

public sealed class CreateTenantHandler
    : IRequestHandler<CreateTenantCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTenantHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateTenantCommand request,
        CancellationToken cancellationToken)
    {
        var tenant = Tenant.Create(
            new TenantName(request.Name),
            new TenantSlug(request.Slug));

        _context.Tenants.Add(tenant);

        await _context.SaveChangesAsync(cancellationToken);

        return tenant.Id;
    }
}