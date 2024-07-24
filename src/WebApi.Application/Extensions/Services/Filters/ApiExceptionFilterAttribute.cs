using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Common.Error;
using FluentValidation;

namespace WebApi.Application.Extensions.Services.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(FileNotFoundException), HandleFileNotFoundException },
            { typeof(BadRequestException), HandleBadRequestException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(UnauthorizeException), HandleUnauthorizedException },
            { typeof(ForbiddenException), HandleForbiddenException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        if (!context.ModelState.IsValid)
            HandleInvalidModelStateException(context);
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type)) _exceptionHandlers[type].Invoke(context);
        else HandleInternalServer(context);
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var errors = new Dictionary<string, IEnumerable<string>>();
        foreach (var item in context.ModelState)
        {
            errors.Add(item.Key, item.Value.Errors.Select(x => x.ErrorMessage));
        }
        var details = new
        {
            Status = StatusCodes.Status400BadRequest,
            Message = "Invalid model",
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = errors
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;
        var details = new
        {
            Status = StatusCodes.Status404NotFound,
            exception.Message,
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Errors = new List<object>()
        };
        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var details = new
        {
            Status = StatusCodes.Status400BadRequest,
            exception.Message,
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = exception.Errors.Select(x => x.ErrorMessage)
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleBadRequestException(ExceptionContext context)
    {
        var exception = (BadRequestException)context.Exception;
        var details = new
        {
            Status = StatusCodes.Status400BadRequest,
            exception.Message,
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = new List<object>()
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleFileNotFoundException(ExceptionContext context)
    {
        var exception = (FileNotFoundException)context.Exception;
        var message = exception.Message;
        var details = new
        {
            Status = StatusCodes.Status400BadRequest,
            Message = string.IsNullOrEmpty(message) ? "File not found" : message,
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = new List<object>()
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleUnauthorizedException(ExceptionContext context)
    {
        var exception = (UnauthorizeException)context.Exception;
        var message = exception.Message;
        var details = new
        {
            Status = StatusCodes.Status401Unauthorized,
            Message = string.IsNullOrEmpty(message) ? "Unauthorized" : message,
            Succedded = false,
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            Errors = new List<object>()
        };
        context.Result = new UnauthorizedObjectResult(details);
        context.ExceptionHandled = true;
    }

    private static void HandleForbiddenException(ExceptionContext context)
    {
        var exception = (ForbiddenException)context.Exception;
        var message = exception.Message;
        var errors = new Dictionary<string, IEnumerable<string>>();
        foreach (var item in context.ModelState)
        {
            errors.Add(item.Key, item.Value.Errors.Select(x => x.ErrorMessage));
        }

        var details = new
        {
            Status = StatusCodes.Status403Forbidden,
            Message = string.IsNullOrEmpty(message) ? "Forbidden" : message,
            Succedded = false,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            Errors = errors
        };
        var forbiddenResult = new BadRequestObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
        context.Result = forbiddenResult;

        context.ExceptionHandled = true;
    }
    private static void HandleInternalServer(ExceptionContext context)
    {
        var details = new
        {
            Status = StatusCodes.Status500InternalServerError,
            Message = "Internal Server",
            Succedded = false,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = new List<object>()
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = false;
    }
}