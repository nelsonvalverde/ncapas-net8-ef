using WebApi.Entities.User.Enums;

namespace WebApi.Entities.User.Dtos;

public sealed record CreateUserDto(
    string Id,
    string Name,
    string LastName,
    string? PhoneNumber,
    string Email,
    string PasswordHash,
    DateOnly Birthday,
    UserStatus StatusId,
    bool EmailConfirmed
);

