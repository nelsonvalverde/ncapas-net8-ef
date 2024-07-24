using Microsoft.Data.SqlClient;
using WebApi.Entities.Session.Dtos;
using WebApi.Entities.Session.Enums;

namespace WebApi.Data.FirstContext.Repositories.Impl.SessionRepository;

public static class SessionMapper
{
    public static SessionDto ToSessionDto(SqlDataReader dataReader)
    {
        return new(
            Id: ReaderConvert.ToString(dataReader, "SESSION_ID"),
            UserId: ReaderConvert.ToString(dataReader, "SESSION_USER_ID"),
            Token: ReaderConvert.ToString(dataReader, "SESSION_TOKEN"),
            RefreshToken: ReaderConvert.ToString(dataReader, "SESSION_REFRESH_TOKEN"),
            Expire: ReaderConvert.ToDateTime(dataReader, "SESSION_EXPIRES"),
            StatusId: (SessionStatus)ReaderConvert.ToShort(dataReader, "SESSION_STATUS_ID"),
            Created: ReaderConvert.ToDateTime(dataReader, "SESSION_CREATED"),
            CreatedBy: ReaderConvert.ToString(dataReader, "SESSION_CREATED_BY"),
            LastModified: ReaderConvert.ToDateTime(dataReader, "SESSION_LAST_MODIFIED"),
            LastModifiedBy: ReaderConvert.ToString(dataReader, "SESSION_LAST_MODIFIED_BY")
        );
    }
}