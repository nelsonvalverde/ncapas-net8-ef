using WebApi.Business.Common.Responses.Base;
using WebApi.Business.UseCases.Users.Common.Dtos;

namespace WebApi.Business.UseCases.Users.Commands.RefreshToken;

public sealed record RefreshTokenCommand() : IRequest<ResponseBase<UserAuthDto>>;