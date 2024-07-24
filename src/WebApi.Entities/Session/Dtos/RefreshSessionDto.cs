using WebApi.Entities.Session.Enums;

namespace WebApi.Entities.Session.Dtos;

public sealed record RefreshSessionDto(
    string RefreshToken,
    SessionStatus StatusId
);