using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.Test.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspectCore.Test.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

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