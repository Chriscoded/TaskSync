using MediatR;
using TaskSync.Application.Abstractions;
using TaskSync.Application.Interfaces;
using TaskSync.Domain.Entities;
using TaskSync.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace TaskSync.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
     RegisterCommand request,
     CancellationToken cancellationToken)
    {
        var tenantExists = await _context.Tenants
            .AnyAsync(x => x.Id == request.TenantId, cancellationToken);

        if (!tenantExists)
        {
            throw new Exception("The specified tenant does not exist.");
            // Or return Result.Failure(DomainErrors.Tenant.NotFound);
        }

        var emailExists = await _context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            throw new Exception("Email is already registered.");
        }

        var user = ApplicationUser.Create(
            request.TenantId,
            request.FirstName,
            request.LastName,
            request.Email);

        user.SetPassword(
            _passwordHasher.Hash(request.Password));

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}