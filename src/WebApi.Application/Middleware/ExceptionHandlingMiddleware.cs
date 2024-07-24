using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApi.Business.Common.Error;
using WebApi.Business.UseCases.Errors.CreateError;

namespace WebApi.Application.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next, 
    IMediator mediator, 
    ILogger<ExceptionHandlingMiddleware> logger,
    CancellationToken cancellationToken = default)
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;
    private readonly IMediator _mediator = mediator;
    public async Task InvokeAsync(HttpContext context)
    {
        var urlApi = GetUrlApi(context.Request);
        try
        {
            _logger.LogInformation("Api {UrlApi} Running", urlApi);
            await _next(context);
            _logger.LogInformation("Api {UrlApi} Finished", urlApi);
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case BadRequestException:
                case NotFoundException:
                case UnauthorizeException:
                case ValidationRequestException:
                case ValidationException:
                case ForbiddenException:
                    break;
                default:
                    var message = ex.Message;
                    _logger.LogError(ex, "Api {UrlApi}:{Message} Disrupted", urlApi, message);
                    var stackTrace = ex.StackTrace;
                    var httpRequest = context.Request;
                    var body = await GetBody(context);
                    var createError = new CreateErrorCommand(
                        App: httpRequest?.Headers["App"].ToString() ?? string.Empty,
                        Type: ex.GetType().ToString(),
                        Message: message,
                        Body: body,
                        Method: httpRequest?.Method ?? string.Empty,
                        Path: httpRequest?.Path ?? string.Empty,
                        QueryString: httpRequest?.QueryString.ToString() ?? string.Empty,
                        UserAgent: httpRequest?.Headers?.UserAgent.ToString() ?? string.Empty,
                        StackTrace: stackTrace ?? string.Empty
                    );
                    _ = _mediator.Send(createError, cancellationToken);
                    break;
            }
        }
    }

    private static async Task<string> GetBody(HttpContext httpContext)
    {
        var request = httpContext?.Request;
        if (request is null) return string.Empty;
        request.Body.Position = 0;
        var reader = new StreamReader(request.Body, Encoding.UTF8);
        var body = await reader.ReadToEndAsync().ConfigureAwait(false);
        return body;
    }

    private static string GetUrlApi(HttpRequest request) {
        return $"{request.Method}:{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}";
    }
}