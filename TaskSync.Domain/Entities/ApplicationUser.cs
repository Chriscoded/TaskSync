using TaskSync.Domain.Enums;
using TaskSync.Domain.Events;
using TaskSync.Domain.ValueObjects;
using TaskSync.SharedKernel.Entities;

namespace TaskSync.Domain.Entities;

public sealed class ApplicationUser : AggregateRoot
{
    private ApplicationUser()
    {
        // Required by EF Core
    }

    private ApplicationUser(
        Guid tenantId,
        string firstName,
        string lastName,
        string email)
    {
        Id = Guid.NewGuid();

        TenantId = tenantId;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email;
        Status = UserStatus.Active;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid TenantId { get; private set; }

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public UserStatus Status { get; private set; }

    public string FullName
        => $"{FirstName} {LastName}";
    public string PasswordHash { get; private set; } = string.Empty;

    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    public static ApplicationUser Create(
        Guid tenantId,
        string firstName,
        string lastName,
        string email)
    {
        var user = new ApplicationUser(
            tenantId,
            firstName,
            lastName,
            email);

        user.AddDomainEvent(
            new UserCreatedDomainEvent(
                user.Id,
                tenantId));

        return user;
    }

    public void ChangeEmail(string newEmail)
    {
        ArgumentNullException.ThrowIfNull(newEmail);

        if (Email == newEmail)
            return;

        var previousEmail = Email;

        Email = newEmail;

        AddDomainEvent(
            new UserEmailChangedDomainEvent(
                Id,
                previousEmail,
                newEmail));
    }

    public void Activate()
    {
        if (Status == UserStatus.Active)
            return;

        Status = UserStatus.Active;

        AddDomainEvent(
            new UserActivatedDomainEvent(Id));
    }

    public void Deactivate()
    {
        if (Status == UserStatus.Inactive)
            return;

        Status = UserStatus.Inactive;

        AddDomainEvent(
            new UserDeactivatedDomainEvent(Id));
    }

}