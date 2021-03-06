using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using ResilienceHTTPClinetwithPolly.PollyInfo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.PollyInfo.Policies
{
    public static class HttpRetryPolicies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetHttpRetryPolicy(ILogger logger, IRetryPolicyConfig retryPolicyConfig)
        {
            return HttpPolicyBuilders.GetBaseBuilder()
                    .OrResult(r => !r.IsSuccessStatusCode)
                    .Or<HttpRequestException>()
                     .WaitAndRetryAsync(retryPolicyConfig.RetryCount,
                        ComputeDuration,
                        (result, timeSpan, retryCount, context) =>
                        {
                            OnHttpRetry(result, timeSpan, retryCount, context, logger);
                        });
        }

        private static void OnHttpRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Polly.Context context, ILogger logger)
        {
            if (result.Result != null)
            {
                logger.LogWarning("Request failed with {StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}", result.Result.StatusCode, timeSpan, retryCount);
            }
            else
            {
                logger.LogWarning("Request failed because network failure. Waiting {timeSpan} before next retry. Retry attempt {retryCount}", timeSpan, retryCount);
            }
        }

        private static TimeSpan ComputeDuration(int input)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, input)) + TimeSpan.FromMilliseconds(new Random().Next(0, 100));
        }
    }
}
