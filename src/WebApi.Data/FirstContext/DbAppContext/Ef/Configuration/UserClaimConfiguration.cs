using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.FirstContext.DbAppContext.Ef.Common;
using WebApi.Entities.UserClaim.Dtos;

namespace WebApi.Data.FirstContext.Configuration;

public sealed class UserClaimConfiguration : IEntityTypeConfiguration<UserClaimEntity>
{
    public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
    {
        builder.ToTable("UserClaim", SchemaConstant.Dbo);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusId)
            .HasConversion<byte>();
    }
}