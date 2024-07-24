using WebApi.Entities.Role;
using WebApi.Entities.RoleClaim.Enums;

namespace WebApi.Entities.RoleClaim;

public sealed class RoleClaimEntity
{
    public RoleClaimEntity()
    {
        Role = default!;
    }

    public string Id { get; set; } = default!;
    public string RoleId { get; set; } = default!;
    public RoleEntity Role { get; set; }
    public string Type { get; set; } = default!;
    public string Value { get; set; } = default!;
    public RoleClaimStatus StatusId { get; set; }
}