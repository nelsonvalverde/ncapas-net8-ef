using WebApi.Business.Common.Responses.Base;
using WebApi.Business.UseCases.Users.Common.Dtos;

namespace WebApi.Business.UseCases.Users.Commands.Auth;

public sealed record AuthCommand(
  string Email,
  string Password
) : IRequest<ResponseBase<UserAuthDto>>;