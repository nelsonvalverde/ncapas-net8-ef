using Microsoft.Extensions.Configuration;
using WebApi.Data.Factory.DbFactory.Impl;

namespace WebApi.Data.SecondContext.DbAppContext.Impl;

public class SecondDbAppContext : DbFactory, ISecondDbAppContext
{
    public SecondDbAppContext(IConfiguration configuration) : base(configuration.GetConnectionString("AnotherConnection") ?? string.Empty)
    {
    }

    public SecondDbAppContext(string connectionString) : base(connectionString)
    {
    }
}