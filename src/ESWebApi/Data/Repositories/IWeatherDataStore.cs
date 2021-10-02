using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESWebApi.Data.Repositories
{
    public interface IWeatherDataStore
    {
        public Task<WeatherForecast> GetAsync();
        public Task<IEnumerable<WeatherForecast>> GetAsync(int range);
        public Task<WeatherForecast> GetAsync(DateTime dateTime);
    }
}
