using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.PollyInfo
{
    public static class HttpPolicyBuilders
    {
        public static PolicyBuilder<HttpResponseMessage> GetBaseBuilder()
        {
            return HttpPolicyExtensions.HandleTransientHttpError();
        }
    }
}
