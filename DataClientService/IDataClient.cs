using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.DataClientService
{
    public interface IDataClient
    {
        Task<List<SampleData>> GetSampleData(string url);
    }
}