using Quartz;
using System.Reflection;

namespace WebApi.Business.Common.Services.QuartzService;

public interface IQuartzService
{
    void Build(IServiceCollectionQuartzConfigurator sq, Assembly assembly);
}