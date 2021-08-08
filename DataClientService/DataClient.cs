using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.DataClientService
{
    public class DataClient : IDataClient
    {
        private readonly IHttpClientFactory _httpFactory;

        public DataClient(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<List<SampleData>> GetSampleData(string url)
        {
            using (HttpClient httpclient = _httpFactory.CreateClient())
            using (HttpResponseMessage response = await httpclient.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //List<SampleData> users = await response.Content.ReadAsAsync<List<SampleData>>();
                    List<SampleData> sampleData =
                        JsonConvert.DeserializeObject<List<SampleData>>(await response.Content.ReadAsStringAsync());
                    return sampleData;
                }
                return null;
            }
        }
    }
}
