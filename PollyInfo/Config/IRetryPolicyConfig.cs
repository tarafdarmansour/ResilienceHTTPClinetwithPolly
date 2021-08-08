using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.PollyInfo.Config
{
    public interface IRetryPolicyConfig
    {
        int RetryCount { get; set; }
    }

}
