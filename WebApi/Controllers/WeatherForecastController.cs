using Application.Common.Authentication.Login;
using Common.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Messaging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IDispatcher _dispatcher;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICommandDispatcher commandDispatcher, IDispatcher dispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
            _dispatcher = dispatcher;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        public async Task<ActionResult<Result>> Test()
        {
            //var x = await _commandDispatcher.Send(new LoginCommand("a", "b"));

            var y = await _dispatcher.Dispatch<LoginCommand, Result<string>>(new LoginCommand("a", "b"));

            var x = await _dispatcher.Dispatch(new LoginCommand("a", "b"));

            return Ok(y);
        }
    }
}
