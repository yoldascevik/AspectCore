using System.Collections.Generic;
using System.Threading.Tasks;
using AspectCore.Test.Web.Logging;

namespace AspectCore.Test.Web.Services
{
    public interface IWeatherForecastService: IAspectDecorated
    {
        [AOPLogging]
        [AOPSecondLogging]
        public IEnumerable<WeatherForecast> GetSummaries();
        
        [AOPLogging]
        public Task<IEnumerable<WeatherForecast>> GetSummariesAsync();
    }
}