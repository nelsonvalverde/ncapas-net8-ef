using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApi.Shared.Services.JwtService.Models;

namespace Test.WebApi.Shared.Behaviours;

public class SharedContext
{
    public readonly string Id;
    public readonly IConfiguration Configuration;
    public readonly IOptions<JwtSetting> JwtSetting;

    public SharedContext()
    {
        var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

        Configuration = configuration.Build();

        JwtSetting = GetJwtSetting(Configuration);

        Id = Guid.NewGuid().ToString("D");
    }

    public string? Token { get; private set; }

    public IServiceProvider GetServiceProvider()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton(Configuration);

        return serviceCollection.BuildServiceProvider();
    }

    public void SetToken(string token)
    {
        Token = token;
    }

    #region private Methods

    private IOptions<JwtSetting> GetJwtSetting(IConfiguration configuration)
    {
        IOptions<JwtSetting> jwtSetting = default!;
        var optionSection = configuration.GetSection("JwtSetting").Get<JwtSetting>();
        if (optionSection is not null)
        {
            var jwtSettingOption = Options.Create(optionSection);
            if (jwtSettingOption is not null)
            {
                jwtSetting = jwtSettingOption;
            }
        }
        return jwtSetting;
    }

    #endregion private Methods
}