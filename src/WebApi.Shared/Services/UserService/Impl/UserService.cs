using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebApi.Shared.Constants;
using WebApi.Shared.Services.UserService.Dtos;

namespace WebApi.Shared.Services.UserService.Impl;

public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccesor = httpContextAccessor;

    public string UserId
    {
        get
        {
            return _httpContextAccesor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Sub)?.Value ?? string.Empty;
        }
    }

    public string RefreshToken
    {
        get
        {
            return _httpContextAccesor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.RefreshToken)?.Value ?? string.Empty;
        }
    }

    public DateTime ExpireTokenUtc
    {
        get
        {
            var expClaim = _httpContextAccesor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.ExpireToken);
            if (expClaim is not null)
            {
                var expUnix = long.Parse(expClaim.Value);
                return DateTimeOffset.FromUnixTimeSeconds(expUnix).DateTime;
            }
            return DateTime.MinValue;
        }
    }

    public string Email
    {
        get
        {
            return _httpContextAccesor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
    }

    public string Role
    {
        get
        {
            return _httpContextAccesor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }

    public UserAuthModel UserAuth
    {
        get
        {
            return new UserAuthModel(
                UserId: UserId,
                Email: Email,
                Role: Role,
                RefreshToken: RefreshToken,
                ExpireTokenUtc: ExpireTokenUtc
            );
        }
    }
}