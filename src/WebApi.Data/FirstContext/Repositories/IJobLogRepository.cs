using WebApi.Entities.JobLog;

namespace WebApi.Data.FirstContext.Repositories;

public interface IJobLogRepository
{
    Task Create(JobLogEntity createJobLog, CancellationToken cancellationToken = default);
}
