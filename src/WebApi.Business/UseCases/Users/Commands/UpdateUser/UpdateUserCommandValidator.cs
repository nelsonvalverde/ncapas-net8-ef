using WebApi.Entities.Users;

namespace WebApi.Business.UseCases.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.IdMax).WithMessage(FluentMessageValidator.MaxLength);
        RuleFor(x => x.Name)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.NameMax).WithMessage(FluentMessageValidator.MaxLength);
        RuleFor(x => x.LastName)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.LastNameMax).WithMessage(FluentMessageValidator.MaxLength);
        RuleFor(x => x.Birthday)
            .NotNull().WithMessage(FluentMessageValidator.NotNull);
    }
}