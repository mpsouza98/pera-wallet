using Microsoft.AspNetCore.Mvc;
using PeraInvest.API.Clients;

namespace PeraInvest.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase {

        private readonly ILogger<UsuariosController> log;
        private readonly UsuarioClient usuarioClient;

        public UsuariosController(ILogger<UsuariosController> logger, UsuarioClient _usuarioClient) {
            log = logger;
            usuarioClient = _usuarioClient;
        }

        //[HttpPost(Name = "PostUsuario")]
        //public Usuario PostUsuario() {
            
        //}

        [HttpGet(Name = "GetUsuario")]
        public IActionResult GetUsuario() {
            log.LogInformation("GetUsuario request received!");

            var response = usuarioClient.getUser();
            return Ok(response.Result);
        }
    }
}
