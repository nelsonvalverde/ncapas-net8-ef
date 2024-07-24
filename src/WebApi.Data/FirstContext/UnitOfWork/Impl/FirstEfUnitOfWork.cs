using WebApi.Data.FirstContext.DbAppContext.Ef;
using WebApi.Data.FirstContext.Repositories;

namespace WebApi.Data.FirstContext.UnitOfWork.Impl;

public class FirstEfUnitOfWork : IFirstEfUnitOfWork
{
    private readonly FirstEfDbContext _firstEfDbContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserClaimRepository _userClaimRepository;
    private readonly IErrorRepository _errorRepository;
    private readonly IJobLogRepository _jobLogRepository;
    private bool _disposed;

    public FirstEfUnitOfWork(
        FirstEfDbContext firstEfDbContext,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IUserClaimRepository userClaimRepository,
        IErrorRepository errorRepository,
        IJobLogRepository jobLogRepository)
    {
        _firstEfDbContext = firstEfDbContext ?? throw new ArgumentNullException(nameof(firstEfDbContext));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        _userClaimRepository = userClaimRepository ?? throw new ArgumentNullException(nameof(userClaimRepository));
        _errorRepository = errorRepository ?? throw new ArgumentNullException(nameof(errorRepository));
        _jobLogRepository = jobLogRepository ?? throw new ArgumentNullException(nameof(jobLogRepository));
    }

    public IUserRepository UserRepository => _userRepository;
    public IUserRoleRepository UserRoleRepository => _userRoleRepository;
    public IUserClaimRepository UserClaimRepository => _userClaimRepository;
    public IErrorRepository ErrorRepository => _errorRepository;
    public IJobLogRepository JobLogRepository => _jobLogRepository;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _firstEfDbContext.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _firstEfDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}