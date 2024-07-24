using Microsoft.Extensions.Configuration;
using WebApi.Data.FirstContext;
using WebApi.Data.SecondContext;

namespace WebApi.Data;

public static class DataServiceExtensions
{
    public static IServiceCollection AddDataServiceExtensions(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddFirstContextExtensions(configuration);
        services.AddSecondContextExtensions();
        return services;
    }
}