namespace TaskSync.Application.Tenants.Queries.GetTenants;

public sealed class TenantDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Slug { get; set; } = default!;
}