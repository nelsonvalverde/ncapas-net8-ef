using WebApi.Entities.Session.Enums;

namespace WebApi.Entities.Session.Dtos;

public sealed record CreateSessionDto(
    string Id,
    string UserId,
    string Token,
    string RefreshToken,
    DateTime Expires,
    SessionStatus StatusId
);