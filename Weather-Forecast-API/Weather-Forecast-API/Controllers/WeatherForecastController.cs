using Microsoft.AspNetCore.Mvc;
using System.Net;
using Weather_Forecast_API.Services;

namespace Weather_Forecast_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        private IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }


        /// <param name="latitude">Latitude</param>
        /// <param name="longitude">Longitude</param>
        [HttpGet("{latitude}/{longitude}")]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
        public IEnumerable<WeatherForecast> Get(string latitude, string longitude)
        {
            return _weatherForecastService.GetForecast(latitude, longitude);
        }
    }
}