using Microsoft.Data.SqlClient;
using WebApi.Data.FirstContext.DbAppContext.AdoNet;
using WebApi.Entities.Error.Dto;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.UserService;
using static WebApi.Data.Factory.DbFactory.Impl.DbSchema;

namespace WebApi.Data.FirstContext.Repositories.Impl.ErrorRepository;

public sealed class ErrorRepository(
    IFirstDbContext dbContext,
    IDateTimeService dateTimeService,
    IUserService userService) : IErrorRepository
{
    private readonly IFirstDbContext _dbContext = dbContext;
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IUserService _userService = userService;

    public async Task<string> CreateError(CreateErrorDto createErrorDto, CancellationToken cancellationToken = default)
    {
        var parameters = new SqlParameter[]
        {
            new ("@ERROR_APP", createErrorDto.App),
            new ("@ERROR_TYPE", createErrorDto.Type),
            new ("@ERROR_MESSAGE", createErrorDto.Message),
            new ("@ERROR_BODY", createErrorDto.Body),
            new ("@ERROR_METHOD", createErrorDto.Method),
            new ("@ERROR_PATH", createErrorDto.Path),
            new ("@ERROR_QUERY_STRING", createErrorDto.QueryString),
            new ("@ERROR_USER_AGENT", createErrorDto.UserAgent),
            new ("@ERROR_STACKTRACE", createErrorDto.StackTrace),
            new ("@ERROR_CREATED", _dateTimeService.GetDateTimeUtc),
            new ("@ERROR_CREATED_BY", _userService.UserId),
        };

        var result = await _dbContext.ExecuteScalarAsync<string>(Schema.log, "PROJ_CREATE_ERROR_SP", parameters, cancellationToken);
        if (result is null) return string.Empty;
        return Convert.ToString(result)!;
    }

    public async Task ClearErrors(CancellationToken cancellationToken = default)
    {
        var parameters = Array.Empty<SqlParameter>();
        await _dbContext.ExecuteNonQueryAsync(Schema.log, "PROJ_CLEAR_ERRORS_SP", parameters, cancellationToken);
    }
}