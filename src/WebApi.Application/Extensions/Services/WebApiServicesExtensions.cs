using System.Reflection;
using WebApi.Application.Extensions.Services.Auth;
using WebApi.Application.Extensions.Services.Config;
using WebApi.Application.Extensions.Services.Cors;
using WebApi.Application.Extensions.Services.Filters;
using WebApi.Business;
using WebApi.Data;
using WebApi.Shared;

namespace WebApi.Application.Extensions.Services;

public static class WebApiServicesExtensions
{
    public static IServiceCollection AddWebApiExtensions(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        #region Service Extensions Layer
        services.AddSharedServiceExtensions(configuration, assembly);
        services.AddDataServiceExtensions(configuration);
        services.AddBusinessServiceExtensions(configuration);
        
        #endregion Service Extensions Layer

        #region Custom

        services.AddSwaggerServiceExtensions(configuration);
        services.AddCorsServiceExtensions(configuration);
        services.AddAuthServiceExtensions(configuration);
        services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        #endregion Custom

        return services;
    }
}