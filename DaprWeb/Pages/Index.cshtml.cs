using Dapr.Client;
using DaprApi;
using DaprGrpc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DaprWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DaprClient _daprClient;

        public IndexModel(DaprClient daprClient)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        }

        public async Task OnGet()
        {
            var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
                HttpMethod.Get,
                "daprapi",
                "weatherforecast/get");

            //var response = await _daprClient.InvokeMethodGrpcAsync<GetWeatherRequest, GetWeatherResponse>(
            //    "daprapi",
            //    "GetWeather", new GetWeatherRequest());

            //var forecasts = new List<WeatherForecast>();

            //foreach (var weatherDetail in response.Data)
            //{
            //    forecasts.Add(new WeatherForecast()
            //    {
            //        Date = DateTime.Parse(weatherDetail.Date),
            //        TemperatureC = weatherDetail.TemperatureC,
            //        TemperatureF = weatherDetail.TemperatureC,
            //        Summary =  weatherDetail.Summary
            //    });
            //}

            var response = await _daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>(
                "daprgrpc",
                "SayHello", new HelloRequest(){Name = "Carl.M"});

            ViewData["WeatherForecastData"] = forecasts;

            ViewData["Data"] = response;
        }
    }
}
