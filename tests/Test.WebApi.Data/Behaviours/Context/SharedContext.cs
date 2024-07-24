using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Data.FirstContext.DbAppContext.AdoNet;
using WebApi.Data.FirstContext.DbAppContext.AdoNet.Impl;
using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Data.FirstContext.Repositories.Impl.ErrorRepository;
using WebApi.Data.FirstContext.Repositories.Impl.JobLogRepository;
using WebApi.Data.FirstContext.Repositories.Impl.SessionRepository;
using WebApi.Data.FirstContext.Repositories.Impl.UserClaimRepository;
using WebApi.Data.FirstContext.Repositories.Impl.UserRepository;
using WebApi.Data.FirstContext.Repositories.Impl.UserRoleRepository;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.UserService;

namespace Test.WebApi.Data.Behaviours.Context;

public class SharedContext
{
    public string Id { get; private set; }
    public readonly IConfiguration Configuration;

    public SharedContext()
    {
        var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

        Configuration = configuration.Build();

        Id = Guid.NewGuid().ToString("D");
    }

    public string? RefreshToken { get; private set; }

    public IServiceProvider GetServiceProvider()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(Configuration);

        SetMocks(serviceCollection);
        SetUnitOfWork(serviceCollection);
        SetDbContext(serviceCollection);
        SetRepositoryCollection(serviceCollection);

        return serviceCollection.BuildServiceProvider();
    }

    public void SetOverrideId(string id)
    {
        Id = id;
    }

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    #region Private Methods

    private void SetMocks(IServiceCollection serviceCollection)
    {
        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(s => s.UserId).Returns(Id);
        serviceCollection.AddScoped(_ => userServiceMock.Object);

        var dateTimeServiceMock = new Mock<IDateTimeService>();
        dateTimeServiceMock.Setup(s => s.GetDateTimeUtc).Returns(DateTime.UtcNow);
        serviceCollection.AddScoped(_ => dateTimeServiceMock.Object);
    }

    private static void SetRepositoryCollection(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IUserRoleRepository, UserRoleRepository>();
        serviceCollection.AddScoped<ISessionRepository, SessionRepository>();
        serviceCollection.AddScoped<IErrorRepository, ErrorRepository>();
        serviceCollection.AddScoped<IUserClaimRepository, UserClaimRepository>();
        serviceCollection.AddScoped<IJobLogRepository, JobLogRepository>();
    }

    private void SetDbContext(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFirstDbContext, FirstDbContext>();
        serviceCollection.AddDbContext<FirstEfDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    }

    private static void SetUnitOfWork(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFirstUnitOfWork, FirstUnitOfWork>();
        serviceCollection.AddScoped<IFirstEfUnitOfWork, FirstEfUnitOfWork>();
    }

    #endregion Private Methods
}