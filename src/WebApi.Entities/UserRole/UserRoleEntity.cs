using WebApi.Entities.Base.Audit;
using WebApi.Entities.Role;
using WebApi.Entities.User;

namespace WebApi.Entities.UserRole.Dtos;

public sealed class UserRoleEntity : AuditEntity
{
    public UserRoleEntity()
    {
        User = default!;
        Role = default!;    
    }

    public string UserId { get; set; } = default!;
    public UserEntity User { get; set; }
    public string RoleId { get; set; } = default!;
    public RoleEntity Role { get; set; }
}