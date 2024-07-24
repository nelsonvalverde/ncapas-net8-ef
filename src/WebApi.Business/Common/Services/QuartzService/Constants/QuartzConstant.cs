namespace WebApi.Business.Common.Services.QuartzService.Constants;

public static class QuartzConstant
{
    public static class JobStatus
    {
        public const string Running = "RUNNING";
        public const string Finished = "FINISHED";
        public const string Disrupted = "DISRUPTED";
    }
}