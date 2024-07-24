using WebApi.Entities.User.Enums;

namespace WebApi.Business.Common.Services.TokenService.Models;

public record UserJwtModel(
    string Id,
    string Name,
    string LastName,
    string FullName,
    string? PhoneNumber,
    string Email,
    bool EmailConfirmed,
    UserStatus StatusId,
    DateOnly Birthday,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
);
