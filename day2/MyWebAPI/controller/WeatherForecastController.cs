using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstApi.Controllers
{
    public class WeatherForecast
    {
        public int Id { get; set; } 
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<WeatherForecast> Forecasts = new List<WeatherForecast>
        {
            new WeatherForecast { Id = 1, Date = DateTime.Now.AddDays(1), TemperatureC = 25, Summary = "Warm" },
            new WeatherForecast { Id = 2, Date = DateTime.Now.AddDays(2), TemperatureC = 18, Summary = "Cool" },
        };

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return Forecasts;
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
        {
            forecast.Id = Forecasts.Count + 1;
            Forecasts.Add(forecast);
            return Ok(forecast);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] WeatherForecast updated)
        {
            var existing = Forecasts.FirstOrDefault(f => f.Id == id);
            if (existing == null) return NotFound();

            existing.Date = updated.Date;
            existing.TemperatureC = updated.TemperatureC;
            existing.Summary = updated.Summary;
            return Ok(existing);
        }

        // DELETE: /WeatherForecast/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var forecast = Forecasts.FirstOrDefault(f => f.Id == id);
            if (forecast == null) return NotFound();

            Forecasts.Remove(forecast);
            return NoContent();
        }
    }
}
