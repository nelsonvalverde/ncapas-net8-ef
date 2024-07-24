using WebApi.Entities.User;

namespace WebApi.Data.FirstContext.Repositories.Impl.UserRepository;

public static class UserEntityMapper
{
    public static IQueryable<UserEntity> ToMapUsers(this IQueryable<UserEntity> source)
    {
        return source.Select(x => new UserEntity
        {
            Id = x.Id,
            Name = x.Name,
            LastName = x.LastName,
            FullName = x.FullName,
            Email = x.Email,
            Birthday = x.Birthday,
            EmailConfirmed = x.EmailConfirmed,
            PhoneNumber = x.PhoneNumber,
            StatusId = x.StatusId,
            Created = x.Created,
            CreatedBy = x.CreatedBy,
            LastModified = x.LastModified,
            LastModifiedBy = x.LastModifiedBy,
        });
    }

}