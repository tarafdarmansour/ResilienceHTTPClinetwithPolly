using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResilienceHTTPClinetwithPolly.DataClientService;

namespace ResilienceHTTPClinetwithPolly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly string sampleurl = "http://sampleaddres.com/sampleservice";
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDataClient _dataClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IDataClient dataClient)
        {
            _logger = logger;
            _dataClient = dataClient;
        }

        [HttpGet]
        public async Task<List<SampleData>> Get()
        {
            return await _dataClient.GetSampleData(sampleurl);
        }
    }
}
