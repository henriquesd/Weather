using CodeTest.Domain.Interfaces;
using CodeTest.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeTest.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private string _apiKey = "3cd60b9dd9a7da0d998b1831eb9c8376";

        public async Task<Weather> Get(string longitude, string latitude, string timestamp)
        {
            decimal parsedLongitude, parsedLatitude;
            DateTime informedDateTime;

            decimal.TryParse(longitude, out parsedLongitude);
            decimal.TryParse(latitude, out parsedLatitude);
            DateTime.TryParse(timestamp, out informedDateTime);

            string url = string.Format("http://api.openweathermap.org/pollution/v1/co/{0},{1}/{2}-{3}-{4}T{5}:{6}:{7}Z.json?appid={8}",
                parsedLatitude, parsedLongitude, informedDateTime.Year, ToDateString(informedDateTime.Month), ToDateString(informedDateTime.Day),
                ToDateString(informedDateTime.Hour), ToDateString(informedDateTime.Minute), ToDateString(informedDateTime.Second), _apiKey);

            var weathersStr = await GetWeather(url);
            var wheater = ConvertToWeather(weathersStr);
            return wheater;
        }

        private Weather ConvertToWeather(string weathers)
        {
            Weather weather = JsonConvert.DeserializeObject<Weather>(weathers);
            return weather;
        }

        private async Task<string> GetWeather(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private string ToDateString(int value)
        {
            if (value < 10) return "0" + value;

            return value.ToString();
        }

    }
}
