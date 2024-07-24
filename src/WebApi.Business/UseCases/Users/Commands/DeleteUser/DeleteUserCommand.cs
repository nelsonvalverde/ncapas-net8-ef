using WebApi.Business.Common.Responses.Base;

namespace WebApi.Business.UseCases.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(string Id) : IRequest<ResponseBase<Unit>>;