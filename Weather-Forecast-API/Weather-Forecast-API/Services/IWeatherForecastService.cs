namespace Weather_Forecast_API.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetForecast(string latitude, string longitude);
    }
}
