using Microsoft.EntityFrameworkCore;
using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Entities.Base;
using WebApi.Entities.User;
using WebApi.Entities.User.Dtos;
using WebApi.Shared.Services.DateTimeService;
using WebApi.Shared.Services.UserService;

namespace WebApi.Data.FirstContext.Repositories.Impl.UserRepository;

public sealed class UserRepository(
    FirstEfDbContext dbContext,
    IDateTimeService dateTimeService,
    IUserService userService) : IUserRepository
{
    private readonly DbSet<UserEntity> _dbSet = dbContext.Set<UserEntity>();
    private readonly IDateTimeService _dateTimeService = dateTimeService;
    private readonly IUserService _userService = userService;

    public async Task CreateAsync(CreateUserDto user, CancellationToken cancellationToken = default)
    {
        var entity = new UserEntity
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Birthday = user.Birthday,
            EmailConfirmed = user.EmailConfirmed,
            StatusId = user.StatusId,
            Created = _dateTimeService.GetDateTimeUtc,
            CreatedBy = _userService.UserId,
            LastModifiedBy = _userService.UserId,
            LastModified = _dateTimeService.GetDateTimeUtc
        };
        await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateUserStatus(UpdateUserStatusDto user, CancellationToken cancellationToken = default)
    {
        var userFind = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
        if (userFind is not null)
        {
            userFind.StatusId = user.StatusId;
            userFind.LastModifiedBy = _userService.UserId;
            userFind.LastModified = _dateTimeService.GetDateTimeUtc;
            _dbSet.Update(userFind);
        }
    }

    public async Task<DataList<UserEntity>> FilterAsync(string? filterName = null, CancellationToken cancellationToken = default)
    {
        var result = _dbSet.AsNoTracking()
            .ToMapUsers()
            .Where(x => x.Name == filterName);
        var list = await result.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new DataList<UserEntity>
        {
            List = list,
            TotalRecord = list.Count
        };
    }

    public async Task<UserEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .ToMapUsers()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<UserEntity?> AuthUserAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .ToMapUsers()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken).ConfigureAwait(false);
    }

    public async Task<DataPage<UserEntity>> GetPageAsync(string? filterName = null, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var userFilter = _dbSet.AsNoTracking()
            .Where(x => (filterName != null && x.Name.Contains(filterName)) || filterName == null);

        var result = userFilter
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToMapUsers();
                    
        var userFilterTotal = await userFilter.CountAsync(cancellationToken).ConfigureAwait(false);
        var list = await result.ToListAsync(cancellationToken).ConfigureAwait(false);
        return new DataPage<UserEntity>()
        {
            List = list,
            TotalRecord = userFilterTotal,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
    }

    public async Task UpdateAsync(UpdateUserDto user, CancellationToken cancellationToken = default)
    {
        var userFind = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);
        if (userFind is not null)
        {
            userFind.Name = user.Name;
            userFind.LastName = user.LastName;
            userFind.Birthday = user.Birthday;
            userFind.LastModifiedBy = _userService.UserId;
            userFind.LastModified = _dateTimeService.GetDateTimeUtc;
            _dbSet.Update(userFind);
        }
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .ToMapUsers()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken).ConfigureAwait(false);
    }
}