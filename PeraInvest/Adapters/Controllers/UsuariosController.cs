using Microsoft.AspNetCore.Mvc;
using NUlid;
using PeraInvest.Adapters.Clients;
using PeraInvest.Adapters.Persistence.Context;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Controllers {
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
        //    log.LogInformation("Requisição");

        //    var usuario = new Usuario { id = Ulid.NewUlid().ToString(), email = "matheus@gmail.com", senha = "senhabosta" }; 
        //    //context.Usuarios.Add(usuario);
        //    context.SaveChanges();

        //    return usuario;
        //}

        [HttpGet(Name = "GetUsuario")]
        public IActionResult GetUsuario() {
            log.LogInformation("GetUsuario request received!");

            var response = usuarioClient.getUser();
            return Ok(response.Result);
        }
    }
}
