using FluentValidation;

namespace TaskSync.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}