using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Weather_Forecast_API.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        static HttpClient client = new HttpClient();

        public IEnumerable<WeatherForecast> GetForecast(string latitude, string longitude)
        {

            List<WeatherForecast> DailyWeather = new List<WeatherForecast>();

            string response = GetTemp(latitude, longitude).Result;

            JObject apiResponse = JObject.Parse(response);
            JObject? dailyData = apiResponse["daily"] as JObject;
            JObject? hourlyData = apiResponse["hourly"] as JObject;

            JArray? timeArray = dailyData["time"] as JArray;
            JArray? maxTempArray = dailyData["temperature_2m_max"] as JArray;
            JArray? minTempArray = dailyData["temperature_2m_min"] as JArray;
            JArray? maxApparentTempArray = dailyData["apparent_temperature_max"] as JArray;
            JArray? minApparentTempArray = dailyData["apparent_temperature_min"] as JArray;
            JArray? precipitationProbabilityArray = dailyData["precipitation_probability_mean"] as JArray;
            JArray? preprecipitationArray = dailyData["precipitation_sum"] as JArray;
            JArray? humidityArray = hourlyData["relativehumidity_2m"] as JArray;

            int[] humidity = humidityArray.Select(i => (int)i).ToArray();

            for (int i = 0; i < timeArray.Count; i++)
            {
                WeatherForecast DayWeather = new WeatherForecast
                {
                    Date = timeArray[i].Value<DateTime>().ToString("dd-MM-yyyy"),
                    Temp_High = maxTempArray[i].Value<string>() + "\u00B0C",
                    Temp_Low = minTempArray[i].Value<string>() + "\u00B0C",
                    App_Temp_High = maxApparentTempArray[i].Value<string>() + "\u00B0C",
                    App_Temp_Low = minApparentTempArray[i].Value<string>() + "\u00B0C",
                    Precipitation_Prob = precipitationProbabilityArray[i].Value<string>() + "%",
                    Precipitation = preprecipitationArray[i].Value<string>() + "mm",
                    Humidity = ((int)humidity.Skip(24*i).Take(24).Average()).ToString() + "%"
            };

                DailyWeather.Add(DayWeather);
            }

            return DailyWeather;
        }

        private static async Task<string> GetTemp(string latitude, string longitude)
        {
            string url = string.Format("https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&hourly=relativehumidity_2m&daily=temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_probability_mean,precipitation_sum&timezone=auto", latitude,longitude);
            HttpResponseMessage response = await client.GetAsync(url);

            string apiResponse = await response.Content.ReadAsStringAsync();

            return apiResponse;

        }
    }
}
