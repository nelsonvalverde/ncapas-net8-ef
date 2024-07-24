namespace WebApi.Application.Middleware;

public sealed class AuthHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        //TO DO
        await _next(context);
    }
}