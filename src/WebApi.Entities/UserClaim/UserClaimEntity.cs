using WebApi.Entities.User;
using WebApi.Entities.UserClaim.Enums;

namespace WebApi.Entities.UserClaim.Dtos;

public sealed class UserClaimEntity
{
    public UserClaimEntity()
    {
        User = default!;
    }

    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public UserEntity User { get; set; }
    public string Type { get; set; } = default!;
    public string Value { get; set; } = default!;
    public UserClaimStatus StatusId { get; set; }
}