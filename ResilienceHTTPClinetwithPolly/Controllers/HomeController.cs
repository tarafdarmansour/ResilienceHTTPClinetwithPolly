using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResilienceHTTPClinetwithPolly.DataClientService;

namespace ResilienceHTTPClinetwithPolly.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {

        private readonly string index = "home/Index";
        private readonly string odd = "home/odd";
        private readonly string ten = "home/ten";
        private readonly string longm = "home/long";
        private readonly string ex = "home/ex";
        private readonly ILogger<HomeController> _logger;
        private readonly IDataClient _dataClient;

        public HomeController(IDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        [HttpGet]
        public async Task<Message> Getindex()
        {
            return await _dataClient.GetSampleData(index);
        }

        [HttpGet]
        public async Task<Message> Getodd()
        {
            return await _dataClient.GetSampleData(odd);
        }

        [HttpGet]
        public async Task<Message> Getten()
        {
            return await _dataClient.GetSampleData(ten);
        }

        [HttpGet]
        public async Task<Message> Getlong()
        {
            return await _dataClient.GetSampleData(longm);
        }

        [HttpGet]
        public async Task<Message> Getex()
        {
            return await _dataClient.GetSampleData(ex);
        }
    }
}
