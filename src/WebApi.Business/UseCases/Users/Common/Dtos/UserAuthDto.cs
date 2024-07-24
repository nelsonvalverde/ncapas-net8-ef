using WebApi.Business.Common.Services.TokenService.Models;

namespace WebApi.Business.UseCases.Users.Common.Dtos;

public sealed record UserAuthDto(UserJwtModel User, string Token, string RefreshToken);