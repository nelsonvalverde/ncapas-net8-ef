using WebApi.Entities.Error.Dto;

namespace WebApi.Data.FirstContext.Repositories;

public interface IErrorRepository
{
    Task<string> CreateError(CreateErrorDto createErrorDto, CancellationToken cancellationToken = default);
    Task ClearErrors(CancellationToken cancellationToken = default);
}
