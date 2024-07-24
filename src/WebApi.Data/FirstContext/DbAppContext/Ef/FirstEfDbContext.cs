using Microsoft.EntityFrameworkCore;
using WebApi.Data.FirstContext.Configuration;
using WebApi.Data.FirstContext.DbAppContext.Ef.Configuration;

namespace WebApi.Data.FirstContext.DbAppContext.Ef;

public class FirstEfDbContext(DbContextOptions<FirstEfDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}