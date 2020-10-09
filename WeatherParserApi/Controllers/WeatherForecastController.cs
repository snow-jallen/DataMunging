using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherParserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            var fileName = @"C:\Users\jallen\Downloads\weather.dat";
            var contents = System.IO.File.ReadAllText(fileName);
            var parsedRows = DataMunging.TemperatureParser.ParseFile(contents);
            var day = DataMunging.TemperatureParser.GetDayWithMinTemperatureSpread(parsedRows);
            return day;
        }
    }
}
