namespace WebApi.Entities.JobLog;

public sealed class JobLogEntity
{
    public string InstanceId { get; set; } = default!;
    public string TriggerGroup { get; set; } = default!;
    public string TriggerName { get; set; } = default!;
    public string JobName { get; set; } = default!;
    public string JobFullPath { get; set; } = default!;
    public string JobCronExpression { get; set; } = default!;
    public DateTime Registered { get; set; }
    public string StatusId { get; set; } = default!;
    public string? JobErrorType { get; set; }
    public string? JobErrorMessage { get; set; }
    public string? JobErrorStacktrace { get; set; }
}