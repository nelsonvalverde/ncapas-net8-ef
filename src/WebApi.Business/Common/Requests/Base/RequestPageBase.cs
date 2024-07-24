namespace WebApi.Business.Common.Requests.Base;

public record RequestPageBase(
    int PageNumber = 1,
    int PageSize = 10
);