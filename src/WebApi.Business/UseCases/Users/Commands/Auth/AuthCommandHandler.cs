using WebApi.Business.Common.Error;
using WebApi.Business.Common.Responses.Base;
using WebApi.Business.Common.Services.TokenService;
using WebApi.Business.UseCases.Users.Common.Dtos;
using WebApi.Business.UseCases.Users.Common.Mappers;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Shared.Services.PasswordHashService;

namespace WebApi.Business.UseCases.Users.Commands.Auth;

public sealed class AuthCommandHandler(
    IFirstEfUnitOfWork efUnitOfWork,
    ITokenService tokenService,
    IEncryptService passwordHashService) : IRequestHandler<AuthCommand, ResponseBase<UserAuthDto>>
{
    private readonly IFirstEfUnitOfWork _efUnitOfWork = efUnitOfWork;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IEncryptService _passwordHashService = passwordHashService;

    public async Task<ResponseBase<UserAuthDto>> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = await _efUnitOfWork.UserRepository.AuthUserAsync(request.Email, cancellationToken);
        var userHashedPassword = user?.PasswordHash ?? string.Empty;
        var passwordIsCorrect = _passwordHashService.VerifyEncrypt(request.Password, userHashedPassword);

        if (user is null || !passwordIsCorrect) throw new UnauthorizeException(MessageValidator.ErrorCredentials);

        var userTokenGenerated = await _tokenService.GenerateUserToken(user.ToUserJwtModel());

        return new ResponseBase<UserAuthDto>(userTokenGenerated);
    }
}