using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;

namespace WebApi.Data.FirstContext.Repositories;

public interface ISessionRepository
{
    Task CreateAsync(CreateSessionDto createSession, CancellationToken cancellationToken = default);

    Task UpdateByRefreshToken(RefreshSessionDto refreshSession, CancellationToken cancellationToken = default);

    Task UpdateStatusByUnexpiredToken(string userId, SessionStatus sessionStatus, CancellationToken cancellationToken = default);

    Task<SessionDto?> GetAsync(string userId, string refreshToken, CancellationToken cancellationToken = default);
}