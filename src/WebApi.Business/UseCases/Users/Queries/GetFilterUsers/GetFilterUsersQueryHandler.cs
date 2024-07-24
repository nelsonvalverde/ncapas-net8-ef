using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.Repositories;
using WebApi.Entities.Base;
using WebApi.Entities.User;

namespace WebApi.Business.UseCases.Users.Queries.GetFilterUsers;

public sealed class GetFilterUsersQueryHandler
    (
        IUserRepository userRepository
    )
    : IRequestHandler<GetFilterUsersQuery, ResponseBase<DataList<UserEntity>>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ResponseBase<DataList<UserEntity>>> Handle(GetFilterUsersQuery request, CancellationToken cancellationToken)
    {
        var userPages = await _userRepository.FilterAsync(request.Filter, cancellationToken);
        return new ResponseBase<DataList<UserEntity>>(userPages);
    }
}