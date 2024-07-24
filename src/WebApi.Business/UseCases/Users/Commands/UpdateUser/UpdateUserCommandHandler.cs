using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.Repositories;
using WebApi.Entities.User.Dtos;

namespace WebApi.Business.UseCases.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler(
    IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, ResponseBase<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResponseBase<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = new UpdateUserDto
        (
            Id: request.Id,
            Name: request.Name,
            LastName: request.LastName,
            Birthday: request.Birthday
        );

        await _userRepository.UpdateAsync(updateUser, cancellationToken);

        return new ResponseBase<Unit>();
    }
}