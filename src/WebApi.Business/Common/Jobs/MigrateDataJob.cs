using Quartz;
using WebApi.Business.Common.Services.QuartzService.Root;

namespace WebApi.Business.Common.Services.JobsService.Impl.Jobs;

public sealed class MigrateDataJob(): IRootQuartzJob
{
    public Task Execute(IJobExecutionContext context)
    {
        int divisor = 100;
        int dividendo = 0;
        var resultado = divisor / dividendo;
        return Task.CompletedTask;
    }
}