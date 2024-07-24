using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.User.Dtos;
using WebApi.Entities.User.Enums;
using WebApi.Shared.Services.PasswordHashService;

namespace WebApi.Business.UseCases.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler(
    IFirstEfUnitOfWork efUnitOfWork,
    IEncryptService passwordHashService
    ) : IRequestHandler<CreateUserCommand, ResponseBase<string>>
{
    private readonly IFirstEfUnitOfWork _efUnitOfWork = efUnitOfWork;
    private readonly IEncryptService _passwordHashService = passwordHashService;

    public async Task<ResponseBase<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var createUser = new CreateUserDto(
            Id: Guid.NewGuid().ToString("D"),
            Name: request.Name,
            LastName: request.LastName,
            PhoneNumber: request.PhoneNumber,
            Birthday: request.Birthday,
            Email: request.Email,
            PasswordHash: _passwordHashService.Encrypt(request.Password),
            StatusId: UserStatus.PendingForConfirmed,
            EmailConfirmed: false
        );

        await _efUnitOfWork.UserRepository.CreateAsync(createUser, cancellationToken);

        return new ResponseBase<string>(data: createUser.Id);
    }
}