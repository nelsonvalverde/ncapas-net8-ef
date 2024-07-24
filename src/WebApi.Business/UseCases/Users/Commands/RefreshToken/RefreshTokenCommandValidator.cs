using Microsoft.Extensions.Configuration;
using WebApi.Business.Common.Error;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.Session.Enums;
using WebApi.Shared.Services.UserService;

namespace WebApi.Business.UseCases.Users.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator
    : AbstractValidator<RefreshTokenCommand>
{
    private readonly IUserService _userService;
    private readonly IFirstUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public RefreshTokenCommandValidator(IUserService userService, IFirstUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        RuleFor(x => x).CustomAsync(async (_, _, cancellationToken) => await HasRefreshToken(cancellationToken));
    }

    private async Task HasRefreshToken(CancellationToken cancellationToken)
    {
        var session = await _unitOfWork.SessionRepository.GetAsync(_userService.UserId, _userService.RefreshToken, cancellationToken);
        if (session is null) throw new ForbiddenException(MessageValidator.RefreshTokenError);
        if (session.StatusId == SessionStatus.Expired) throw new ForbiddenException(MessageValidator.RefreshTokenExpired);
        if (session.StatusId == SessionStatus.Revoked) throw new ForbiddenException(MessageValidator.RefreshTokenRevoked);
        var dateTimeExpireToken = _userService.ExpireTokenUtc;
        var refreshTokenBeforeMinutes = _configuration.GetSection("JwtSetting").GetValue<int>("RefreshTokenBeforeMinutes");
        var timeRemaining = dateTimeExpireToken - DateTime.UtcNow;
        if (!(timeRemaining.TotalMinutes > 0 && timeRemaining.TotalMinutes <= refreshTokenBeforeMinutes))
        {
            throw new ForbiddenException(MessageValidator.RefreshTokenDenegate);
        }
    }
}