using Microsoft.AspNetCore.Mvc;
using PeraInvest.Adapters.Persistence.Context;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase {

        private readonly ILogger<UsuariosController> log;
        private readonly UsuarioContext context;

        public UsuariosController(ILogger<UsuariosController> logger, UsuarioContext usuarioContext) {
            log = logger;
            context = usuarioContext;
        }

        [HttpPost(Name = "PostUsuario")]
        public Usuario PostUsuario() {
            log.LogInformation("Requisição");

            var usuario = new Usuario{ id = 1, email = "matheus@gmail.com", senha = "senhabosta"};
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            
            return usuario;
        }
    }
}
