using Test.WebApi.Business.Behaviours.Context;
using WebApi.Shared.Services.PasswordHashService;

namespace Test.WebApi.Business.Behaviours;

public class RootTest : IClassFixture<SharedContext>
{
    private readonly SharedContext _sharedContext;
    private readonly IFirstUnitOfWork _unitOfWork;
    private readonly IFirstEfUnitOfWork _efUnitOfWork;
    private readonly IEncryptService _passwordHashService;

    public RootTest(SharedContext sharedContext)
    {
        var serviceProvider = sharedContext.GetServiceProvider();
        _unitOfWork = serviceProvider.GetService<IFirstUnitOfWork>()!;
        _efUnitOfWork = serviceProvider.GetService<IFirstEfUnitOfWork>()!;
        _passwordHashService = new PasswordHashService();
        _sharedContext = sharedContext;
    }

    protected SharedContext SharedContext => _sharedContext;
    protected IFirstUnitOfWork UnitOfWork => _unitOfWork;
    protected IFirstEfUnitOfWork EfUnitOfWork => _efUnitOfWork;
    protected IEncryptService PasswordHashService => _passwordHashService;
}