using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.FirstContext.DbAppContext.Ef.Common;
using WebApi.Entities.Role;

namespace WebApi.Data.FirstContext.DbAppContext.Ef.Configuration;

public sealed class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Role", SchemaConstant.Dbo);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusId)
            .HasConversion<byte>();
    }
}