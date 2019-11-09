using System;
using System.Collections.Generic;

namespace CodeTest.Domain.Models
{
    public class Weather
    {
        public DateTime Time { get; set; }
        public Location Location { get; set; }
        public List<WeatherData> Data { get; set; }
    }
}