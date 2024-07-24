namespace WebApi.Shared.Services.DateTimeService;

public interface IDateTimeService
{
    /// <summary>
    /// Gets the get date time UTC.
    /// </summary>
    /// <value>
    /// The get date time UTC.
    /// </value>
    DateTime GetDateTimeUtc { get; }
}