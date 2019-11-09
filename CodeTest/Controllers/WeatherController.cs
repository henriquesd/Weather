using CodeTest.Domain.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace CodeTest.Controllers
{
    public class WeatherController : ApiController
    {
        private WeatherService _weatherService;

        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        public async Task<IHttpActionResult> Get(string longitude, string latitude, string timestamp)
        {
            if (!LongitudeIsValid(longitude)) return BadRequest("Invalid longitude");
            if (!LatitudeIsValid(latitude)) return BadRequest("Invalid latitude");
            if (!TimestampIsValid(timestamp)) return BadRequest("Invalid timestamp. Format is yyyy-MM-ddThh:mm:ss");

            DateTime informedDateTime;
            if (!DateTime.TryParse(timestamp, out informedDateTime)) return BadRequest("Invalid date time values");

            var result = await _weatherService.Get(longitude, latitude, timestamp);

            return Ok(result);
        }

        private bool TimestampIsValid(string timestamp)
        {
            Regex regex = new Regex(@"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})T(?<hours>\d{2}):(?<minutes>\d{2}):(?<seconds>\d{2})");
            Match match = regex.Match(timestamp);
            return match.Success;
        }

        private bool LatitudeIsValid(string latitude)
        {
            decimal parsedLatitude;
            return decimal.TryParse(latitude, out parsedLatitude) || parsedLatitude < -90 || parsedLatitude > 90;
        }

        private bool LongitudeIsValid(string longitude)
        {
            decimal parsedLongitude;
            return decimal.TryParse(longitude, out parsedLongitude) || parsedLongitude < -180 || parsedLongitude > 180;
        }
    }
}
