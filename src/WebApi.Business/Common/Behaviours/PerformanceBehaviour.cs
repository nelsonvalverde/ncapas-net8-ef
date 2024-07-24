using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApi.Shared.Services.UserService;

namespace WebApi.Business.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger logger,
    IUserService userService) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer = new();
    private readonly ILogger _logger = logger;
    private readonly IUserService _userService = userService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var warning = $"[WebApi.Application] - Long Running Request {requestName} ({elapsedMilliseconds} milliseconds), by {_userService.UserId}";
            _logger.LogWarning(warning);
        }
        return response;
    }
}