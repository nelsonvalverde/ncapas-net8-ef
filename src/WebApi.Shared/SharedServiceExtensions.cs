using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.DateTimeService.Impl;
using WebApi.Shared.Services.JwtService;
using WebApi.Shared.Services.JwtService.Impl;
using WebApi.Shared.Services.PasswordHashService;
using WebApi.Shared.Services.ReflectionService;
using WebApi.Shared.Services.ReflectionService.Impl;
using WebApi.Shared.Services.UserService;
using WebApi.Shared.Services.UserService.Impl;

namespace WebApi.Shared;

public static class SharedServiceExtensions
{
    public static IServiceCollection AddSharedServiceExtensions(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IEncryptService, PasswordHashService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IReflectionService, ReflectionService>();
        return services;
  
    }
}