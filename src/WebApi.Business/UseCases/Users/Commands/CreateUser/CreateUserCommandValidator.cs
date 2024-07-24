using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.Users;

namespace WebApi.Business.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IFirstEfUnitOfWork efUnitOfWork)
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.NameMax).WithMessage(FluentMessageValidator.MaxLength);
        RuleFor(x => x.LastName)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.LastNameMax).WithMessage(FluentMessageValidator.MaxLength);
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(FluentMessageValidator.ValidateEmail)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.EmailMax).WithMessage(FluentMessageValidator.MaxLength)
            .MustAsync(async (currentEmail, cancellationToken) =>
            {
                var user = await efUnitOfWork.UserRepository.GetUserByEmailAsync(currentEmail, cancellationToken);
                return user?.Email != currentEmail;
            }).WithMessage(MessageValidator.EmailExist);
        RuleFor(x => x.Birthday)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty);
    }
}