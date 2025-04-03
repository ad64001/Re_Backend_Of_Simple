using Microsoft.AspNetCore.Mvc;
using Re_Backend.Common.SqlConfig;
using Re_Backend.Domain;

namespace Re_Backend.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ITestDbService testService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestDbService testService)
    {
        _logger = logger;
        this.testService = testService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return testService.DoSomething();
    }
}
