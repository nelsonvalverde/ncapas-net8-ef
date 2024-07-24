using WebApi.Data.FirstContext.Repositories;

namespace WebApi.Data.FirstContext.UnitOfWork;

public interface IFirstEfUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IErrorRepository ErrorRepository { get; }
    IUserClaimRepository UserClaimRepository { get; }
    IJobLogRepository JobLogRepository { get; }

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}