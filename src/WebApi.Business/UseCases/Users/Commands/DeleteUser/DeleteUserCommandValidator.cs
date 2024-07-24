using WebApi.Entities.Users;

namespace WebApi.Business.UseCases.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage(FluentMessageValidator.NotNull)
            .NotEmpty().WithMessage(FluentMessageValidator.NotEmpty)
            .MaximumLength(UserTable.IdMax).WithMessage(FluentMessageValidator.MaxLength);
    }
}