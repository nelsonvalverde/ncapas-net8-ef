using WebApi.Business.Common.Responses.Base;
using WebApi.Entities.Base;
using WebApi.Entities.User;

namespace WebApi.Business.UseCases.Users.Queries.GetFilterUsers;

public sealed record GetFilterUsersQuery(
    string Filter
) : IRequest<ResponseBase<DataList<UserEntity>>>;