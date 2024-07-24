using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.FirstContext.DbAppContext.Ef.Common;
using WebApi.Entities.User;

namespace WebApi.Data.FirstContext.DbAppContext.Ef.Configuration;

public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User", SchemaConstant.Dbo);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusId)
            .HasConversion<byte>();

        builder.Property(x => x.FullName)
            .HasComputedColumnSql();
    }
}