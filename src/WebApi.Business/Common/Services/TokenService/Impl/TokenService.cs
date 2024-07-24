using System.Security.Claims;
using WebApi.Business.Common.Services.TokenService.Models;
using WebApi.Business.UseCases.Users.Common.Dtos;
using WebApi.Business.UseCases.Users.Common.Mappers;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;
using WebApi.Shared.Services.JwtService;
using WebApi.Shared.Services.JwtService.Models;

namespace WebApi.Business.Common.Services.TokenService.Impl;

public sealed class TokenService(
    IFirstUnitOfWork unitOfWork,
    IFirstEfUnitOfWork efUnitOfWork,
    IJwtService jwtService
    ) : ITokenService
{
    private readonly IFirstUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFirstEfUnitOfWork _efUnitOfWork = efUnitOfWork;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<UserAuthDto> GenerateUserToken(UserJwtModel user)
    {
        var customClaims = await GetClaims(user.Id);
        var tokenGenerated = _jwtService.GenerateToken(new JwtUser(user.Id, user.Name, user.Email), customClaims);
        var userAuth = new UserAuthDto(user.ToUserAuth(), tokenGenerated.Token, tokenGenerated.RefreshToken);
        await _unitOfWork.SessionRepository.CreateAsync(new CreateSessionDto(
            Id: Guid.NewGuid().ToString("D"),
            UserId: user.Id,
            Token: tokenGenerated.Token,
            RefreshToken: tokenGenerated.RefreshToken,
            StatusId: SessionStatus.Active,
            Expires: tokenGenerated.Expires
        ));

        return userAuth;
    }

    public async Task<IEnumerable<Claim>> GetClaims(string userId)
    {
        var userClaimsAsync = _efUnitOfWork.UserClaimRepository.GetUserClaims(userId);
        var roleClaimsAsync = _efUnitOfWork.UserRoleRepository.GetRoleClaimsByUser(userId);

        await Task.WhenAll(userClaimsAsync, roleClaimsAsync);

        var userClaims = await userClaimsAsync;
        var roleClaims = await roleClaimsAsync;

        return [
            ..userClaims.Select(x => new Claim(x.Type, x.Value)),
            ..roleClaims.Select(x => new Claim(x.Type, x.Value))
        ];
    }
}