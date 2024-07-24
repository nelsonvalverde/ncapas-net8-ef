using WebApi.Business.Common.Responses.Base;

namespace WebApi.Business.UseCases.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    string Id,
    string Name,
    string LastName,
    DateOnly Birthday
): IRequest<ResponseBase<Unit>>;