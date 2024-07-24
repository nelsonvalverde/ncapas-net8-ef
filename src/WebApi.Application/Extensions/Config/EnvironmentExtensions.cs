namespace WebApi.Application.Extensions.Config;

public static class EnvironmentExtensions
{
    public static IConfigurationBuilder AddEnvironmentExtensions(this IConfigurationBuilder builder, IHostEnvironment env)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
        return builder;

    }
}