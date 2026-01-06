using Polly;

namespace MainService.BLL.Resilience;

public static class TimeoutPolicy
{
    public static ResiliencePipelineBuilder AddDefaultTimeout(this ResiliencePipelineBuilder builder)
    {
        return builder.AddTimeout(TimeSpan.FromSeconds(20));
    }
}