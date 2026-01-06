using Polly;
using Polly.CircuitBreaker;

namespace MainService.BLL.Resilience;

public static class CircuitBreakerPolicy
{
    public static ResiliencePipelineBuilder AddDefaultCircuitBreaker(this ResiliencePipelineBuilder builder)
    {
        return builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions
        {
            FailureRatio = 0.5,
            MinimumThroughput = 10,
            SamplingDuration = TimeSpan.FromSeconds(30),
            BreakDuration = TimeSpan.FromSeconds(15)
        });
    }
}