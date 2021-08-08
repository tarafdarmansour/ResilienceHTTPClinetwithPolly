using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.PollyInfo.Config
{
    public class PolicyConfig : ICircuitBreakerPolicyConfig, IRetryPolicyConfig
    {
        public int RetryCount { get; set; }
        public int BreakDuration { get; set; }
    }
}
