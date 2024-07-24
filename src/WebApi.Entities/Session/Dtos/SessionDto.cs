using WebApi.Entities.Session.Enums;

namespace WebApi.Entities.Session.Dtos;

public sealed record SessionDto(
    string Id,
    string UserId,
    string Token,
    string RefreshToken,
    DateTime Expire,
    SessionStatus StatusId,
    string CreatedBy,
    DateTime Created,
    string LastModifiedBy,
    DateTime LastModified
);