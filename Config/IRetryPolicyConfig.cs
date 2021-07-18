using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.Config
{
    public interface IRetryPolicyConfig
    {
        int RetryCount { get; set; }
    }

}
