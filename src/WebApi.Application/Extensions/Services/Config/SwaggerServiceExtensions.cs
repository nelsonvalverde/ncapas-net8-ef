using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace WebApi.Application.Extensions.Services.Config;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        string schemeRt = "JwtId", schemeJwt = "Bearer";

        var appSection = configuration.GetSection("App");
        var appName = appSection.GetValue<string>("Name") ?? string.Empty;
        var appUrl = appSection.GetValue<string>("Url") ?? string.Empty;

        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = schemeJwt,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
        };

        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = schemeJwt
                    }
                },
                Array.Empty<string>()
            }
            
        };


        var contact = new OpenApiContact()
        {
            Name = appName,
            Email = "nelsonvalverdelt@outlook.com",
            Url = new Uri(appUrl)
        };

        var license = new OpenApiLicense()
        {
            Name = appName,
            Url = new Uri(appUrl)
        };

        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = $"API - {appName}",
            Description = "Api Detail",
            Contact = contact,
            License = license
        };

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", info);
            o.AddSecurityDefinition(schemeJwt, securityScheme);
            o.AddSecurityRequirement(securityReq);
            o.CustomSchemaIds((type) => type.FullName);
        });

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true
        );

        return services;
    }
}
