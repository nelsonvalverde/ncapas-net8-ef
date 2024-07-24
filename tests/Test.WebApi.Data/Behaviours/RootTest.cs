using Test.WebApi.Data.Behaviours.Context;

namespace Test.WebApi.Data.Behaviours;

public class RootTest : IClassFixture<SharedContext>
{
    private readonly SharedContext _sharedContext;
    private readonly IFirstUnitOfWork _unitOfWork;
    private readonly IFirstEfUnitOfWork _efUnitOfWork;

    public RootTest(SharedContext sharedContext)
    {
        var serviceProvider = sharedContext.GetServiceProvider();
        _unitOfWork = serviceProvider.GetService<IFirstUnitOfWork>()!;
        _efUnitOfWork = serviceProvider.GetService<IFirstEfUnitOfWork>()!;
        _sharedContext = sharedContext;
    }

    protected SharedContext SharedContext => _sharedContext;
    protected IFirstUnitOfWork UnitOfWork => _unitOfWork;
    protected IFirstEfUnitOfWork EfUnitOfWork => _efUnitOfWork;
}