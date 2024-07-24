using Microsoft.Extensions.Configuration;
using Quartz;
using System.Reflection;
using WebApi.Business.Common.Services.QuartzService.Root;
using WebApi.Shared.Services.ReflectionService;

namespace WebApi.Business.Common.Services.QuartzService.Impl;

public class QuartzService(
    IReflectionService reflectionService,
    IConfiguration configuration
    ) : IQuartzService
{
    private readonly IReflectionService _reflectionService = reflectionService;
    private readonly IConfiguration _configuration = configuration;

    public void Build(IServiceCollectionQuartzConfigurator sq, Assembly assembly)
    {
        var sectionJob = _configuration.GetSection("Jobs");
        var typesImpInterfaces = _reflectionService.GetClassTypes<IRootQuartzJob>(assembly);

        foreach (var type in typesImpInterfaces)
        {
            var nameTypeClass = type.Name;
            var sectionChildJob = sectionJob.GetSection(nameTypeClass);
            var cronSchedule = sectionChildJob.GetValue<string>("CronSchedule");

            if (cronSchedule is not null)
            {
                var jobKey = new JobKey(nameTypeClass);
                sq.AddJob(type, jobKey);

                sq.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{nameTypeClass}Trigger")
                .WithCronSchedule(cronSchedule));
            }
        }
    }
}