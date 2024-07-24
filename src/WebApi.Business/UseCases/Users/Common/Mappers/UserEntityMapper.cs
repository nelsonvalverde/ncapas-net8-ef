using WebApi.Business.Common.Services.TokenService.Models;
using WebApi.Entities.User;

namespace WebApi.Business.UseCases.Users.Common.Mappers;

public static class UserEntityMapper
{
    public static UserJwtModel ToUserJwtModel(this UserEntity user)
    {
        return new UserJwtModel(
            Id: user.Id,
            Name: user.Name,
            FullName: user.FullName,
            LastName: user.LastName,
            PhoneNumber: user.PhoneNumber,
            Email: user.Email,
            EmailConfirmed: user.EmailConfirmed,
            StatusId: user.StatusId,
            Birthday: user.Birthday,
            CreatedBy: user.CreatedBy,
            Created: user.Created,
            LastModifiedBy: user.LastModifiedBy,
            LastModified: user.LastModified
        );
    }
}