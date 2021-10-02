using ESWebApi;
using Xunit;

namespace ESWebApiTests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void CanConvertCelsiusToFahrenheit()
        {
            // Arrange
            var weatherForecast = new WeatherForecast
            {
                TemperatureC = 20
            };

            // Act
            var temperatureF = weatherForecast.TemperatureF;

            // Assert
            Assert.Equal(67, temperatureF);
        }
    }
}
