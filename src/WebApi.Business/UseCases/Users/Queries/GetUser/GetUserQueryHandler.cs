using WebApi.Business.Common.Error;
using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.User;

namespace WebApi.Business.UseCases.Users.Queries.GetUser;

public sealed class GetUserQueryHandler(
        IFirstEfUnitOfWork efUnitOfWork
    )
    : IRequestHandler<GetUserQuery, ResponseBase<UserEntity>>
{
    private readonly IFirstEfUnitOfWork _efUnitOfWork = efUnitOfWork;

    public async Task<ResponseBase<UserEntity>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _efUnitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);
        return user is null ? throw new NotFoundException("User doest not exist") : new ResponseBase<UserEntity>(user);
    }
}