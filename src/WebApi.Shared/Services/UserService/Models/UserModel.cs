namespace WebApi.Shared.Services.UserService.Dtos;

public record UserAuthModel
(
    string UserId,
    string Email,
    string Role,
    string RefreshToken,
    DateTime ExpireTokenUtc
);