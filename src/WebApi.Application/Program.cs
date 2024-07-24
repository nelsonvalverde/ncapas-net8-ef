using WebApi.Application.Extensions.Config;
using WebApi.Application.Extensions.Serilog;
using WebApi.Application.Extensions.Services;
using WebApi.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region Custom
builder.AddSerilogExtensions();
builder.Configuration.AddEnvironmentExtensions(builder.Environment);
builder.Services.AddWebApiExtensions(builder.Configuration, builder.Environment);
#endregion

var app = builder.Build();

// Enable Buffering for to catch body from request
app.Use(next => context =>
{
    context.Request.EnableBuffering();
    return next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<AuthHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();