using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Test.Web.Logging;
using AspectCore.Test.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspectCore.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.GetSummaries();
        }
        
        [HttpGet]
        [Route("GetSummariesAsync")]
        public async Task<IEnumerable<WeatherForecast>> GetSummariesAsync()
        {
            return await _weatherForecastService.GetSummariesAsync();
        }
    }
}