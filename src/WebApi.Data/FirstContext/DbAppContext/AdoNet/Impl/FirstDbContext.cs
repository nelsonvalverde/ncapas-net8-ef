using Microsoft.Extensions.Configuration;
using WebApi.Data.Factory.DbFactory.Impl;

namespace WebApi.Data.FirstContext.DbAppContext.AdoNet.Impl;

public class FirstDbContext : DbFactory, IFirstDbContext
{
    public FirstDbContext(IConfiguration configuration) : base(configuration.GetConnectionString("DefaultConnection") ?? string.Empty)
    {

    }

    public FirstDbContext(string connectionString) : base(connectionString)
    {

    }
}
