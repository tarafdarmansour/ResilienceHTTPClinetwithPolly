using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using ResilienceHTTPClinetwithPolly.PollyInfo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.PollyInfo.Policies
{
    public class HttpCircuitBreakerPolicies
    {
        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy(ILogger logger, ICircuitBreakerPolicyConfig circuitBreakerPolicyConfig)
        {
            return HttpPolicyBuilders.GetBaseBuilder()
                     .CircuitBreakerAsync(circuitBreakerPolicyConfig.RetryCount + 1,
                        TimeSpan.FromSeconds(circuitBreakerPolicyConfig.BreakDuration),
                        (result, breakDuration) =>
                        {
                            OnHttpBreak(result, breakDuration, circuitBreakerPolicyConfig.RetryCount, logger);
                        },
                        () =>
                        {
                            OnHttpReset(logger);
                        });
        }

        public static void OnHttpBreak(DelegateResult<HttpResponseMessage> result, TimeSpan breakDuration, int retryCount, ILogger logger)
        {
            logger.LogWarning("Service shutdown during {breakDuration} after {DefaultRetryCount} failed retries.", breakDuration, retryCount);
            throw new BrokenCircuitException("Service inoperative. Please try again later");
        }

        public static void OnHttpReset(ILogger logger)
        {
            logger.LogInformation("Service restarted.");
        }
    }
}
