using WebApi.Entities.Users;

namespace WebApi.Business.UseCases.Users.Commands.Auth;
public sealed class AuthCommandValidator : AbstractValidator<AuthCommand>
{
    public AuthCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(FluentMessageValidator.ValidateEmail)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.EmailMax).WithMessage(FluentMessageValidator.MaxLength);

        RuleFor(x => x.Password)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.PasswordHashMax).WithMessage(FluentMessageValidator.MaxLength);

    }
}
