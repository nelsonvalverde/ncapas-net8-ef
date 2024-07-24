using Microsoft.EntityFrameworkCore;
using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Entities.UserClaim.Dtos;

namespace WebApi.Data.FirstContext.Repositories.Impl.UserClaimRepository;

public sealed class UserClaimRepository(
    FirstEfDbContext dbContext
    ) : IUserClaimRepository
{
    private readonly DbSet<UserClaimEntity> _dbSet = dbContext.Set<UserClaimEntity>();

    public async Task<IEnumerable<UserClaimEntity>> GetUserClaims(string userId, CancellationToken cancellationToken = default)
    {
        var result = _dbSet.AsNoTracking().Where(x => x.UserId == userId);
        return await result.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}