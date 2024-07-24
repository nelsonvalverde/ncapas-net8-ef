using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.FirstContext.DbAppContext.Ef.Common;
using WebApi.Entities.UserRole.Dtos;

namespace WebApi.Data.FirstContext.Configuration;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRole", SchemaConstant.Dbo);
        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}