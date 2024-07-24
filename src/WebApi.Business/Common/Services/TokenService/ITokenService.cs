using System.Security.Claims;
using WebApi.Business.Common.Services.TokenService.Models;
using WebApi.Business.UseCases.Users.Common.Dtos;

namespace WebApi.Business.Common.Services.TokenService;

public interface ITokenService
{
    Task<UserAuthDto> GenerateUserToken(UserJwtModel user);

    Task<IEnumerable<Claim>> GetClaims(string userId);
}