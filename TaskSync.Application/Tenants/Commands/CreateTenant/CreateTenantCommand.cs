using MediatR;

namespace TaskSync.Application.Tenants.Commands.CreateTenant;

public sealed record CreateTenantCommand(
    string Name,
    string Slug
) : IRequest<Guid>;