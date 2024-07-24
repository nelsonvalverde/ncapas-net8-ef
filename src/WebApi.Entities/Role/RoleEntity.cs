using WebApi.Entities.Base.Audit;
using WebApi.Entities.Role.Enums;
using WebApi.Entities.RoleClaim;

namespace WebApi.Entities.Role;

public sealed class RoleEntity : AuditEntity
{
    public RoleEntity()
    {
        RoleClaims = [];
    }

    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public RoleStatus StatusId { get; set; }
    public ICollection<RoleClaimEntity> RoleClaims { get; set; }
}