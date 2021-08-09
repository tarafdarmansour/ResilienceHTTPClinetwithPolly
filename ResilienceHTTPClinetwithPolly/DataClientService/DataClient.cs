using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataModel;

namespace ResilienceHTTPClinetwithPolly.DataClientService
{
    public class DataClient: IDataClient
    {
        private readonly HttpClient _client;

        public DataClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Message> GetSampleData(string url)
        {
            using (HttpResponseMessage response = await _client.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //List<SampleData> users = await response.Content.ReadAsAsync<List<SampleData>>();
                    var sampleData =
                        JsonConvert.DeserializeObject<Message>(await response.Content.ReadAsStringAsync());
                    return sampleData;
                }

                return new Message();
            }
        }
    }
}
