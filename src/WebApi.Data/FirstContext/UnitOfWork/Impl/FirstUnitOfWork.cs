using WebApi.Data.FirstContext.DbAppContext.AdoNet;
using WebApi.Data.FirstContext.Repositories;

namespace WebApi.Data.FirstContext.UnitOfWork.Impl;

public class FirstUnitOfWork : IFirstUnitOfWork
{
    private readonly IFirstDbContext _dbContext;
    private readonly ISessionRepository _sessionRepository;
    private readonly IErrorRepository _errorRepository;
    private bool _disposed;

    public FirstUnitOfWork(
        IFirstDbContext dbContext,
        ISessionRepository sessionRepository,
        IErrorRepository errorRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbContext.OpenConnectionAndKeepOpen();
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        _errorRepository = errorRepository ?? throw new ArgumentNullException(nameof(errorRepository));
    }

    public ISessionRepository SessionRepository => _sessionRepository;
    public IErrorRepository ErrorRepository => _errorRepository;

    #region Methods

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.DisposeConnection();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion Methods
}