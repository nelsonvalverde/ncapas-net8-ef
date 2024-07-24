using WebApi.Data.FirstContext.Repositories;

namespace WebApi.Data.FirstContext.UnitOfWork;

public interface IFirstUnitOfWork : IDisposable
{
    ISessionRepository SessionRepository { get; }
    IErrorRepository ErrorRepository { get; }
}