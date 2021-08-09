using DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResilienceHTTPClinetwithPolly.DataClientService
{
    public interface IDataClient
    {
        Task<Message> GetSampleData(string url);
    }
}