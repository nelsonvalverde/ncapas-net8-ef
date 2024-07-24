using Microsoft.Data.SqlClient;
using WebApi.Data.Factory.DbFactory.Impl;
using WebApi.Data.FirstContext.DbAppContext.AdoNet;
using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.UserService;

namespace WebApi.Data.FirstContext.Repositories.Impl.SessionRepository;

public sealed class SessionRepository(
    IFirstDbContext dbContext,
    IDateTimeService dateTimeService,
    IUserService userService) : ISessionRepository
{
    private readonly IFirstDbContext _dbContext = dbContext;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IUserService _userService = userService;

    public async Task CreateAsync(CreateSessionDto createSession, CancellationToken cancellationToken = default)
    {
        var parameters = new SqlParameter[]
        {
            new("@ID", createSession.Id),
            new("@USER_ID", createSession.UserId),
            new("@TOKEN", createSession.Token),
            new("@REFRESH_TOKEN", createSession.RefreshToken),
            new("@EXPIRES", createSession.Expires),
            new("@STATUS_ID", createSession.StatusId),
            new("@CREATED", _dateTimeService.GetDateTimeUtc),
            new("@CREATED_BY", _userService.UserId),
            new("@LAST_MODIFIED_BY", _userService.UserId),
            new("@LAST_MODIFIED", _dateTimeService.GetDateTimeUtc),
        };

        await _dbContext.ExecuteNonQueryAsync(DbSchema.Schema.aud, "PROJ_CREATE_SESSION_SP", parameters, cancellationToken);
    }

    public async Task UpdateByRefreshToken(RefreshSessionDto refreshSession, CancellationToken cancellationToken = default)
    {
        var parameters = new SqlParameter[]
        {
            new("@REFRESH_TOKEN", refreshSession.RefreshToken),
            new("@STATUS_ID", refreshSession.StatusId),
            new("@LAST_MODIFIED_BY", _userService.UserId),
            new("@LAST_MODIFIED", _dateTimeService.GetDateTimeUtc),
        };

        await _dbContext.ExecuteNonQueryAsync(DbSchema.Schema.aud, "PROJ_UPDATE_SESSION_BY_REFRESH_TOKEN_SP", parameters, cancellationToken);
    }

    public async Task UpdateStatusByUnexpiredToken(string userId, SessionStatus sessionStatus, CancellationToken cancellationToken = default)
    {
        var parameters = new SqlParameter[]
        {
            new("@STATUS_ID", sessionStatus),
            new("@LAST_MODIFIED_BY", userId),
            new("@LAST_MODIFIED", _dateTimeService.GetDateTimeUtc),
        };

        await _dbContext.ExecuteNonQueryAsync(DbSchema.Schema.aud, "PROJ_UPDATE_SESSION_STATUS_BY_UNEXPIRED_TOKEN_SP", parameters, cancellationToken);
    }

    public async Task<SessionDto?> GetAsync(string userId, string refreshToken, CancellationToken cancellationToken = default)
    {
        SessionDto? sessionDto = null;
        var parameters = new SqlParameter[]
        {
            new("@USER_ID", userId),
            new("@REFRESH_TOKEN", refreshToken),
        };

        await _dbContext.DataReaderAsync(
           schema: DbSchema.Schema.aud,
           storedProcedure: "PROJ_GET_SESSION_BY_USER_ID_AND_REFRESH_TOKEN_SP",
           readerAction: (reader) =>
           {
               sessionDto = SessionMapper.ToSessionDto(reader);
           },
           parameters: parameters,
           cancellationToken: cancellationToken
        );

        return sessionDto;
    }
}