using WebApi.Shared.Services.UserService.Dtos;

namespace WebApi.Shared.Services.UserService;

/// <summary>
/// Get token data
/// </summary>
public interface IUserService
{
    string UserId { get; }
    string RefreshToken { get; }
    string Email { get; }
    string Role { get; }
    DateTime ExpireTokenUtc { get; }
    UserAuthModel UserAuth { get; }
}