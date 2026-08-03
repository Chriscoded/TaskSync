
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskSync.Application.Abstractions;

namespace TaskSync.Application.Tenants.Commands.CreateTenant;

public sealed class CreateTenantCommandValidator
    : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator(
        IApplicationDbContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Slug)
            .MustAsync(async (slug, cancellation) =>
            {
                return !await context.Tenants.AnyAsync(
                    x => x.Slug.Value == slug,
                    cancellation);
            })
            .WithMessage("Tenant slug already exists.");
    }
}