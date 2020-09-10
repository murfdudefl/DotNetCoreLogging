using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreLogging.Controllers
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
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			}).ToArray();

			//The Logging Levels
			var todaySummary = forecast[0].Summary;
			_logger.LogTrace("This is a trace log - Today's forcast:  {forecast}", todaySummary);
			_logger.LogDebug("This is a debug log - Today's forcast:  {forecast}", todaySummary);
			_logger.LogInformation("This is an info log - Today's forcast:  {forecast}", todaySummary);
			_logger.LogWarning("This is a warning log - Today's forcast:  {forecast}", todaySummary);
			_logger.LogError("This is an error log - Today's forcast:  {forecast}", todaySummary);

			try
			{
				throw new ApplicationException("Something really bad happened!", new InvalidOperationException("This is definitely invalid"));
			}
			catch(Exception ex)
			{
				_logger.LogCritical(ex, "Controller failed!");
			}

			return forecast;
		}
	}
}
