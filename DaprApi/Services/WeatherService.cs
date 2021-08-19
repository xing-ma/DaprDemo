using System;
using System.Globalization;
using System.Linq;
using DaprApi.Proto;
using Grpc.Core;
using System.Threading.Tasks;

namespace DaprApi.Services
{
    public class WeatherService : Weather.WeatherBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override async Task<GetWeatherResponse> GetWeather(GetWeatherRequest request, ServerCallContext context)
        {
            var rng = new Random();

            await Task.Delay(10);

            var response = new GetWeatherResponse();

            Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList().ForEach(x =>
            {
                response.Data.Add(new WeatherDetail()
                {
                    Date = x.Date.ToString(CultureInfo.InvariantCulture),
                    TemperatureC = x.TemperatureC,
                    Summary = x.Summary
                });
            });
            

            return response;
        }
    }
}
