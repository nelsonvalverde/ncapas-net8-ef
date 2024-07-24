using WebApi.Data.SecondContext.DbAppContext;
using WebApi.Data.SecondContext.Repositories;

namespace WebApi.Data.SecondContext.UnitOfWork.Impl;

public sealed class SecondUnitOfWork(
    ISecondDbAppContext dbContext,
    IEntityRepository entityRepository) : ISecondUnitOfWork
{
    private readonly ISecondDbAppContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IEntityRepository _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));

    public IEntityRepository EntityRepository => _entityRepository;

    #region Methods


    #endregion Methods
}