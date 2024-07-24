using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Shared.Services.JwtService;

namespace WebApi.Application.Extensions.Services.Auth;

public static class AuthServiceExtensions
{
    public static IServiceCollection AddAuthServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvdier = services.BuildServiceProvider();
        var jwtService = serviceProvdier.GetService<IJwtService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             if (jwtService is not null) options.TokenValidationParameters = jwtService.GetTokenValidationParameters();
         });

        return services;
    }
}