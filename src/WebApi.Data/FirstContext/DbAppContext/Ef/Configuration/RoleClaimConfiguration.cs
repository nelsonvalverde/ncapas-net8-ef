using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.FirstContext.DbAppContext.Ef.Common;
using WebApi.Entities.RoleClaim;

namespace WebApi.Data.FirstContext.Configuration;

public sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
{
    public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
    {
        builder.ToTable("RoleClaim", SchemaConstant.Dbo);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusId)
            .HasConversion<byte>();

    }
}