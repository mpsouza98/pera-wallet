using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using PeraInvest.API.Commands;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Infrastructure;

namespace PeraInvest.API.Controllers {

    [Route("[controller]")]
    [ApiController]
    public class AtivoFinanceiroController : ControllerBase {
        private readonly IMediator mediator;
        private readonly ILogger<AtivoFinanceiroController> logger;

        public AtivoFinanceiroController(IMediator mediator, ILogger<AtivoFinanceiroController> logger) {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<Results<Ok<AtivoFinanceiro>, BadRequest<CriarAtivoCommand>>> PostCarteira([FromBody] CriarAtivoCommand criarAtivoCommand) {
            logger.LogInformation("Criando novo ativo financeiro");
            
            var result = await mediator.Send(criarAtivoCommand);
            logger.LogInformation("Sucesso no comando de criar ativo financeiro");

            return TypedResults.Ok(result);
        }
    }
}
