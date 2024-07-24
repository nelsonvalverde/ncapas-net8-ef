using WebApi.Business.Common.Responses.Base;

namespace WebApi.Business.UseCases.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string LastName,
    string? PhoneNumber,
    string Email,
    string Password,
    DateOnly Birthday
) : IRequest<ResponseBase<string>>;