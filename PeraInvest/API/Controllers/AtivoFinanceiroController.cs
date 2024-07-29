using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeraInvest.API.Commands;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Infrastructure;

namespace PeraInvest.API.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class AtivoFinanceiroController : ControllerBase {
        private readonly IMediator _mediator;
        private readonly ILogger<AtivoFinanceiroController> _logger;

        public AtivoFinanceiroController(IMediator mediator, ILogger<AtivoFinanceiroController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AtivoFinanceiro>> PostCarteira([FromBody] CriarAtivoCommand criarAtivoCommand) {
            _logger.LogInformation("Criando novo ativo financeiro");
            
            return await _mediator.Send(criarAtivoCommand);
        }
    }
}
