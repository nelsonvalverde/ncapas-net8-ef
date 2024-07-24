using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using WebApi.Business.Common.Services.QuartzService.Constants;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.JobLog;

namespace WebApi.Business.Common.Services.QuartzService.Behaviours;

public sealed class JobTimingBehabiour(
        ILogger<JobTimingBehabiour> logger,
        IServiceScopeFactory serviceScopeFactory
    ) : IJobListener
{
    private readonly IServiceScopeFactory _asyncServiceScope = serviceScopeFactory;
    private readonly ILogger<JobTimingBehabiour> _logger = logger;
    public string Name => "Web Api Job Listener";

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        using var scopeFactory = _asyncServiceScope.CreateAsyncScope();
        using var unitOfWork = scopeFactory.ServiceProvider.GetService<IFirstEfUnitOfWork>();
        var jobLog = GetJobLog(context, QuartzConstant.JobStatus.Running);
        _logger.LogInformation("{StatusId}: {JobName}", jobLog.StatusId, jobLog.JobName);
        if (unitOfWork is not null)
        {
            await unitOfWork.JobLogRepository.Create(jobLog, cancellationToken);
        }
    }

    public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        using var scopeFactory = _asyncServiceScope.CreateAsyncScope();
        using var unitOfWork = scopeFactory.ServiceProvider.GetService<IFirstEfUnitOfWork>();

        if (jobException is null)
        {
            var jobLog = GetJobLog(context, QuartzConstant.JobStatus.Finished, jobException);
            _logger.LogInformation("{StatusId}: {JobName}", jobLog.StatusId, jobLog.JobName);
            if (unitOfWork is not null)
                await unitOfWork.JobLogRepository.Create(jobLog, cancellationToken);
        }
        else
        {
            var jobLog = GetJobLog(context, QuartzConstant.JobStatus.Disrupted, jobException);
            _logger.LogError(jobException, "{StatusId}: {JobName}", jobLog.StatusId, jobLog.JobName);
            if (unitOfWork is not null)
                await unitOfWork.JobLogRepository.Create(jobLog, cancellationToken);
        }
    }

    private static JobLogEntity GetJobLog(IJobExecutionContext context, string statusId, JobExecutionException? jobException = null)
    {
        var triggerKey = context.Trigger.Key;
        var triggerName = triggerKey.Name;
        var triggerGroup = triggerKey.Group;

        var jobInstanceId = context.FireInstanceId;
        var jobName = context.JobDetail.JobType.Name;
        var jobFullPath = context.JobDetail.JobType.FullName;
        var jobCronExpression = (context.Trigger as ICronTrigger)?.CronExpressionString;

        string? errorMessage = null, errorStackTrace = null, errorType = null;
        if (jobException is not null)
        {
            var errorException = jobException.InnerException?.InnerException;

            errorMessage = errorException?.Message;
            errorStackTrace = errorException?.StackTrace;
            errorType = errorException?.GetType().ToString();
        }

        return new JobLogEntity
        {
            InstanceId = jobInstanceId,
            TriggerGroup = triggerGroup,
            TriggerName = triggerName,
            JobName = jobName,
            JobFullPath = jobFullPath ?? string.Empty,
            JobCronExpression = jobCronExpression ?? string.Empty,
            Registered = DateTime.UtcNow,
            StatusId = statusId,
            JobErrorType = errorType,
            JobErrorMessage = errorMessage,
            JobErrorStacktrace = errorStackTrace
        };
    }
}