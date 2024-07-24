using WebApi.Business.Common.Error;
using WebApi.Business.Common.Responses.Base;
using WebApi.Business.Common.Services.TokenService;
using WebApi.Business.UseCases.Users.Common.Dtos;
using WebApi.Business.UseCases.Users.Common.Mappers;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;
using WebApi.Shared.Services.UserService;

namespace WebApi.Business.UseCases.Users.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IFirstUnitOfWork unitOfWork,
    IFirstEfUnitOfWork efUnitOfWork,
    IUserService userService,
    ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, ResponseBase<UserAuthDto>>
{
    private readonly IFirstUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFirstEfUnitOfWork _efUnitOfWork = efUnitOfWork;
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<ResponseBase<UserAuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _efUnitOfWork.UserRepository.GetByIdAsync(_userService.UserId, cancellationToken);
        if (user is null) throw new ForbiddenException(MessageValidator.UserDoestnExist);
        var token = await _tokenService.GenerateUserToken(user.ToUserJwtModel());
        await _unitOfWork.SessionRepository.UpdateByRefreshToken(new RefreshSessionDto(
            RefreshToken: _userService.RefreshToken,
            StatusId: SessionStatus.Expired
        ), cancellationToken);
        return new ResponseBase<UserAuthDto>(token);
    }
}