using Microsoft.EntityFrameworkCore;
using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Entities.JobLog;

namespace WebApi.Data.FirstContext.Repositories.Impl.JobLogRepository;

public sealed class JobLogRepository(
    FirstEfDbContext dbContext
) : IJobLogRepository
{
    private readonly DbSet<JobLogEntity> _dbSet = dbContext.Set<JobLogEntity>();

    public async Task Create(JobLogEntity createJobLog, CancellationToken cancellationToken = default)
    {
        var entity = new JobLogEntity
        {
            InstanceId = createJobLog.InstanceId,
            TriggerGroup = createJobLog.TriggerGroup,
            TriggerName = createJobLog.TriggerName,
            JobName = createJobLog.JobName,
            JobFullPath = createJobLog.JobFullPath,
            JobCronExpression = createJobLog.JobCronExpression,
            Registered = createJobLog.Registered,
            StatusId = createJobLog.StatusId,
            JobErrorType = createJobLog.JobErrorType,
            JobErrorMessage = createJobLog.JobErrorMessage,
            JobErrorStacktrace = createJobLog.JobErrorStacktrace
        };
        await _dbSet.AddAsync(entity, cancellationToken);
    }
}