namespace TaskSync.Domain.Enums;

public enum UserRole
{
    SystemAdministrator = 1,
    TenantAdministrator = 2,
    ProjectManager = 3,
    Contributor = 4,
    Auditor = 5,
    Support = 6
}