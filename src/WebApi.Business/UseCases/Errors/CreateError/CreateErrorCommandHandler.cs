using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.Repositories;
using WebApi.Entities.Error.Dto;

namespace WebApi.Business.UseCases.Errors.CreateError;

public sealed class CreateErrorCommandHandler(IErrorRepository errorRepository)
    : IRequestHandler<CreateErrorCommand, ResponseBase<Unit>>
{
    private readonly IErrorRepository _errorRepository = errorRepository;

    public async Task<ResponseBase<Unit>> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
    {
        await _errorRepository.CreateError(new CreateErrorDto(
            App: request.App,
            Type: request.Type,
            Message: request.Message,
            Body: request.Body,
            Method: request.Method,
            Path: request.Path,
            QueryString: request.QueryString,
            UserAgent: request.UserAgent,
            StackTrace: request.StackTrace
        ), cancellationToken);
        return new ResponseBase<Unit>();
    }
}