using WebApi.Entities.Base.Audit;
using WebApi.Entities.User.Enums;
using WebApi.Entities.UserClaim.Dtos;

namespace WebApi.Entities.User;

public sealed class UserEntity : AuditEntity
{
    public UserEntity()
    {
        UserClaims = [];
    }

    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public DateOnly Birthday { get; set; }
    public UserStatus StatusId { get; set; }
    public bool EmailConfirmed { get; set; }
    public IEnumerable<UserClaimEntity> UserClaims { get; set; }
}