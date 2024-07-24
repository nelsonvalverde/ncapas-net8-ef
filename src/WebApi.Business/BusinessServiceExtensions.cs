using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;
using WebApi.Business.Common.Behaviours;
using WebApi.Business.Common.Services.QuartzService.Impl;
using WebApi.Business.Common.Services.QuartzService;
using WebApi.Business.Common.Services.TokenService;
using WebApi.Business.Common.Services.TokenService.Impl;
using WebApi.Business.Common.Services.QuartzService.Behaviours;


namespace WebApi.Business;

public static class BusinessServiceExtensions
{
    public static IServiceCollection AddBusinessServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        #region Providers
        var assembly = Assembly.GetExecutingAssembly();
      
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        #endregion Providers

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IQuartzService, QuartzService>();

        #region Quartz
        var jobsIsEnabled =  Convert.ToBoolean(configuration.GetSection("Jobs").GetValue<string>("Enabled"));
        if (jobsIsEnabled)
        {
            services.AddQuartz(q =>
            {
                using var serviceProvider = services.BuildServiceProvider();
                serviceProvider.GetService<IQuartzService>()?.Build(q, assembly);
                q.AddJobListener<JobTimingBehabiour>();
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        }
        #endregion
        return services;
    }
}