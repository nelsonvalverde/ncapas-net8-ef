using WebApi.Entities.Base;
using WebApi.Entities.RoleClaim;
using WebApi.Entities.UserRole.Dtos;

namespace WebApi.Data.FirstContext.Repositories;

public interface IUserRoleRepository
{
    Task<IEnumerable<RoleClaimEntity>> GetRoleClaimsByUser(string userId, CancellationToken cancellationToken = default);

    Task AssignRoleToUser(AssignUserRoleDto assign, CancellationToken cancellationToken = default);

    Task<DataList<UserRoleEntity>> GetRolesByUser(string userId, CancellationToken cancellationToken = default);
}