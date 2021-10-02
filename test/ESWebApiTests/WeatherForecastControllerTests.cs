using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESWebApi;
using ESWebApi.Controllers;
using ESWebApi.Data.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ESWebApiTests
{
    // Based on the official Microsoft Docs.
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0

    public class WeatherForecastControllerTests
    {
        [Fact]
        public async Task DefaultGetReturnsFromRepository()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var mockRepository = new Mock<IWeatherDataStore>();
            mockRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestWeatherForecasts());
            var controller = new WeatherForecastController(mockLogger.Object, mockRepository.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Contains(result, r => r.Summary == "Summary A");
            Assert.Contains(result, x => x.Summary == "Summary B");
            Assert.Contains(result, x => x.Summary == "Summary C");
        }

        private static List<WeatherForecast> GetTestWeatherForecasts() => new List<WeatherForecast>
        {
            new WeatherForecast
            {
                Date = new DateTime(2021, 10, 1),
                Summary = "Summary A",
                TemperatureC = 10
            },

            new WeatherForecast
            {
                Date = new DateTime(2021, 10, 2),
                Summary = "Summary B",
                TemperatureC = 10
            },

            new WeatherForecast
            {
                Date = new DateTime(2021, 10, 3),
                Summary = "Summary C",
                TemperatureC = 10
            }
        };
    }
}
