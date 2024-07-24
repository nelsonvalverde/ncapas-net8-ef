using WebApi.Entities.Base;
using WebApi.Entities.User;
using WebApi.Entities.User.Dtos;

namespace WebApi.Data.FirstContext.Repositories;

public interface IUserRepository
{ 
    Task CreateAsync(CreateUserDto user, CancellationToken cancellationToken = default);

    Task UpdateAsync(UpdateUserDto user, CancellationToken cancellationToken = default);

    Task UpdateUserStatus(UpdateUserStatusDto user, CancellationToken cancellationToken = default);

    Task<UserEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<UserEntity?> AuthUserAsync(string email, CancellationToken cancellationToken = default);

    Task<DataPage<UserEntity>> GetPageAsync(string? filterName = null, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<DataList<UserEntity>> FilterAsync(string? filterName = null, CancellationToken cancellationToken = default);
}