namespace WebApi.Shared.Services.DateTimeService.Impl;

public class DateTimeService : IDateTimeService
{
    public DateTime GetDateTimeUtc => DateTime.UtcNow;
}