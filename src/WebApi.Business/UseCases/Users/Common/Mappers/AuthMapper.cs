﻿using WebApi.Business.Common.Services.TokenService.Models;

namespace WebApi.Business.UseCases.Users.Common.Mappers;

public static class AuthMapper
{
    public static UserJwtModel ToUserAuth(this UserJwtModel user)
    {
        return user with { PasswordHash = null };
    }

}