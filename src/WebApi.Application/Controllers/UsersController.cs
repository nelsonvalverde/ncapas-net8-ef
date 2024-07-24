using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Common.Responses.Base;
using WebApi.Business.UseCases.Users.Commands.Auth;
using WebApi.Business.UseCases.Users.Commands.CreateUser;
using WebApi.Business.UseCases.Users.Commands.RefreshToken;
using WebApi.Business.UseCases.Users.Common.Dtos;

namespace WebApi.Application.Controllers;

public class UsersController : ApiController
{
    [AllowAnonymous]
    [HttpPost("Auth")]
    [ProducesResponseType<ResponseBase<UserAuthDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Auth(AuthCommand auth)
    {
        return Ok(await Mediator.Send(auth));
    }

    [HttpPost("RefreshToken")]
    [ProducesResponseType<ResponseBase<UserAuthDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand refreshToken)
    {
        return Ok(await Mediator.Send(refreshToken));
    }
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType<ResponseBase<UserAuthDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreateUserCommand createUser)
    {
        return Ok(await Mediator.Send(createUser));
    }
}