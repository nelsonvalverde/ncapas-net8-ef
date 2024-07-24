using WebApi.Business.Common.Responses.Base;
using WebApi.Entities.Session.Dtos;

namespace WebApi.Business.UseCases.Users.Queries.GetSession;

public sealed record GetSessionQuery() : IRequest<ResponseBase<SessionDto>>;