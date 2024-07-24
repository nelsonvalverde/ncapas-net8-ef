namespace WebApi.Application.Extensions.Services.Cors;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddCorsServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var myAllowSpecificOrigins = "_myAllowSpecificOrigins_webapi_2024";

        var corsAllowSpecificOrigins = configuration.GetSection("Cors");
        var corsUrls = corsAllowSpecificOrigins.GetValue<string>("Urls") ?? string.Empty;
        var corsMethods = corsAllowSpecificOrigins.GetValue<string>("Methods") ?? string.Empty;

        var listCorsUrls = corsUrls.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var listMethods = corsMethods.Split(",", StringSplitOptions.RemoveEmptyEntries);
        services.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins(listCorsUrls);
                    policy.WithMethods(listMethods);
                });
        });

        return services;    
    }
}