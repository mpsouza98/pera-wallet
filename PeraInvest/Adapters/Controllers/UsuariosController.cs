using Microsoft.AspNetCore.Mvc;

namespace PeraInvest.Adapters.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase {

        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger) {
            _logger = logger;
        }

        [HttpPost(Name = "PostUsuario")]
        public void<WeatherForecast> Get() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
