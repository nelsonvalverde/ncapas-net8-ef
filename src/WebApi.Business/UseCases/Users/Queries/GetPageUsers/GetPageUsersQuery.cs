using WebApi.Business.Common.Requests.Base;
using WebApi.Business.Common.Responses.Base;
using WebApi.Entities.Base;
using WebApi.Entities.User;
using WebApi.Entities.User.Dtos;

namespace WebApi.Business.UseCases.Users.Queries.GetPageUsers;

public record GetPageUsersQuery(
    string? FilterName,
    int PageNumber,
    int PageSize
) : RequestPageBase(PageNumber, PageSize), IRequest<ResponseBase<DataPage<UserEntity>>>;