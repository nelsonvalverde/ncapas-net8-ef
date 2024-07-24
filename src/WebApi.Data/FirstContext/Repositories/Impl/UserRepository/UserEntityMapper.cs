using WebApi.Entities.User;

namespace WebApi.Data.FirstContext.Repositories.Impl.UserRepository;

public static class UserEntityMapper
{
    public static IQueryable<UserEntity> ToMapUsers(this IQueryable<UserEntity> source, bool ignorePassword = true)
    {
        return source.Select(x => new UserEntity
        {
            Id = x.Id,
            Name = x.Name,
            LastName = x.LastName,
            FullName = x.FullName,
            Email = x.Email,
            Birthday = x.Birthday,
            PasswordHash =  ignorePassword ? string.Empty : x.PasswordHash,
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