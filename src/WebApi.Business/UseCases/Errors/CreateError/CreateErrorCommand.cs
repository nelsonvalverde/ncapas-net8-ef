using WebApi.Business.Common.Responses.Base;

namespace WebApi.Business.UseCases.Errors.CreateError;

public sealed record CreateErrorCommand(
    string App,
    string Type,
    string Message,
    string Body,
    string Method,
    string Path,
    string QueryString,
    string UserAgent,
    string StackTrace
    ) : IRequest<ResponseBase<Unit>>
{ }