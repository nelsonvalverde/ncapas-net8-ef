using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.Repositories;
using WebApi.Entities.User.Dtos;
using WebApi.Entities.User.Enums;

namespace WebApi.Business.UseCases.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler(
        IUserRepository userRepository
    ): IRequestHandler<DeleteUserCommand, ResponseBase<Unit>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResponseBase<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var deleteUser = new UpdateUserStatusDto
        (
            Id: request.Id,
            StatusId: UserStatus.Deleted
        );

        await _userRepository.UpdateUserStatus(deleteUser, cancellationToken);

        return new ResponseBase<Unit>();
    }
}