using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESWebApi.Data.Repositories
{
    public class InMemoryWeatherDataStore : IWeatherDataStore
    {
        private readonly WeatherForecast[] _weatherData;

        public InMemoryWeatherDataStore()
        {
            _weatherData = GetMockData();
        }

        private static WeatherForecast[] GetMockData()
        {
            var rng = new Random();
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            return Enumerable.Range(1, 14).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            })
           .ToArray();
        }

        /// <summary>
        /// Gets the earliest future forecast.
        /// </summary>
        /// <returns>WeatherForecast item.</returns>
        public Task<WeatherForecast> GetAsync() => GetAsync(DateTime.Now);

        /// <summary>
        /// Gets the first forecast after the provided datetime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>WeatherForecast item.</returns>
        public Task<WeatherForecast> GetAsync(DateTime dateTime)
        {
            var result = _weatherData
                .Where(wd => wd.Date > dateTime)
                .OrderBy(wd => wd.Date)
                .Last();

            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets the earliest future forecasts for the given date range.
        /// </summary>
        /// <param name="range">Count of days in the future.</param>
        /// <returns>WeatherForecast items.</returns>
        public Task<IEnumerable<WeatherForecast>> GetAsync(int range)
        {
            var result = _weatherData
                .Where(wd => wd.Date > DateTime.Now)
                .OrderBy(wd => wd.Date)
                .Take(range);

            return Task.FromResult(result);
        }
    }
}
