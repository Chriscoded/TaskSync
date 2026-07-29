using TaskSync.SharedKernel.Results;

public static class DomainErrors
{
    public static readonly Error TenantNotFound =
        new("Tenant.NotFound", "Tenant not found.");
}