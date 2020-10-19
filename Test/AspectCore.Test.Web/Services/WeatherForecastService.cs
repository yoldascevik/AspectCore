using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AspectCore.Test.Web.Services
{
    public class WeatherForecastService: IWeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;
        
        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        public IEnumerable<WeatherForecast> GetSummaries()
        {
            _logger.LogInformation("GetSummaries called!");
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        public async Task<IEnumerable<WeatherForecast>> GetSummariesAsync()
        {
            _logger.LogInformation("GetSummariesAsync called!");
            return await Task.FromResult(GetSummaries());
        }
    }
}