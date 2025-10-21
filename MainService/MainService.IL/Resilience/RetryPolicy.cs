using Polly;
using Polly.Retry;

namespace MainService.BLL.Resilience;

public static class RetryPolicy
{
    public static ResiliencePipelineBuilder AddDefaultRetry(this ResiliencePipelineBuilder builder)
    {
        return builder.AddRetry(new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            Delay = TimeSpan.FromSeconds(2),
            MaxRetryAttempts = 3,
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true
        });
    }
}