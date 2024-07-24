using Quartz;
using WebApi.Business.Common.Services.QuartzService.Root;
using WebApi.Data.FirstContext.UnitOfWork;

namespace WebApi.Business.Common.Services.JobsService.Impl.Jobs;

public sealed class ClearErrorLogsJob (
    IFirstUnitOfWork unitOfWork) : IRootQuartzJob
{
    private readonly IFirstUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(IJobExecutionContext context)
    {
        await _unitOfWork.ErrorRepository.ClearErrors();
    }
}