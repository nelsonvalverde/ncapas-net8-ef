using WebApi.Business.Common.Error;
using WebApi.Business.Common.Responses.Base;
using WebApi.Data.FirstContext.UnitOfWork;
using WebApi.Entities.Session.Dtos;
using WebApi.Shared.Services.UserService;

namespace WebApi.Business.UseCases.Users.Queries.GetSession;

public sealed class GetSessionQueryHandler(
        IUserService userService,
        IFirstUnitOfWork unitOfWork
    )
    : IRequestHandler<GetSessionQuery, ResponseBase<SessionDto>>
{
    private readonly IUserService _userService = userService;
    private readonly IFirstUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseBase<SessionDto>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var session = await _unitOfWork.SessionRepository.GetAsync(_userService.UserId, _userService.RefreshToken, cancellationToken);
        if (session is null) throw new ForbiddenException(MessageValidator.RefreshTokenDoesntExist);
        return new ResponseBase<SessionDto>(session);
    }
}