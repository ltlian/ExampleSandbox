using System.Collections.Generic;
using System.Threading.Tasks;
using ESWebApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherDataStore _weatherDataStore;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDataStore weatherDataStore)
        {
            _logger = logger;
            _weatherDataStore = weatherDataStore;
        }

        [HttpGet]
        public Task<IEnumerable<WeatherForecast>> Get()
        {
            return _weatherDataStore.GetAsync(5);
        }
    }
}
