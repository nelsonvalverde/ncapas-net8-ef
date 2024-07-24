using WebApi.Entities.User.Enums;

namespace WebApi.Entities.User.Dtos;

public sealed record UpdateUserStatusDto(
    string Id,
    UserStatus StatusId);