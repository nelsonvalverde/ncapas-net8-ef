using Microsoft.EntityFrameworkCore;
using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Entities.Base;
using WebApi.Entities.RoleClaim;
using WebApi.Entities.UserRole.Dtos;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.UserService;

namespace WebApi.Data.FirstContext.Repositories.Impl.UserRoleRepository;

public sealed class UserRoleRepository(
    FirstEfDbContext dbContext,
    IDateTimeService dateTimeService,
    IUserService userService) : IUserRoleRepository
{
    private readonly DbSet<UserRoleEntity> _dbSet = dbContext.Set<UserRoleEntity>();
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IUserService _userService = userService;

    public async Task<IEnumerable<RoleClaimEntity>> GetRoleClaimsByUser(string userId, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet.AsNoTracking()
                .Include(x => x.Role)
                .ThenInclude(x => x.RoleClaims)
                .FirstAsync(x => x.UserId == userId, cancellationToken);

        if (result is null) return [];
        return result.Role.RoleClaims;
    }

    public async Task AssignRoleToUser(AssignUserRoleDto assign, CancellationToken cancellationToken = default)
    {
        var entity = new UserRoleEntity
        {
            UserId = assign.UserId,
            RoleId = assign.RoleId,
            Created = _dateTimeService.GetDateTimeUtc,
            CreatedBy = _userService.UserId,
            LastModifiedBy = _userService.UserId,
            LastModified = _dateTimeService.GetDateTimeUtc
        };
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task<DataList<UserRoleEntity>> GetRolesByUser(string userId, CancellationToken cancellationToken = default)
    {
        var result = _dbSet.AsNoTracking().Where(x => x.UserId == userId);
        var list = await result.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new DataList<UserRoleEntity>
        {
            List = list,
            TotalRecord = list.Count
        };
    }
}