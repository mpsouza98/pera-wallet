using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeraInvest.API.Commands;
using PeraInvest.API.Commands.Handlers;

namespace PeraInvest.API.Controllers {

    [Route("/api/ativos_financeiro")]
    [ApiController]
    public class AtivoFinanceiroController : ControllerBase {
        private readonly IMediator mediator;
        //private readonly AtivoFinanceiroQuery ativoFinanceiroQuery;
        private readonly ILogger<AtivoFinanceiroController> logger;

        public AtivoFinanceiroController(IMediator mediator, ILogger<AtivoFinanceiroController> logger) {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CriarAtivoFinanceiroResponse>> PostAtivoFinanceiro([FromBody] CriarAtivoCommand criarAtivoCommand) {
            logger.LogInformation("Criando novo ativo financeiro");
            
            var result = await mediator.Send(criarAtivoCommand);
            logger.LogInformation("Sucesso no comando de criar ativo financeiro");

            return CreatedAtAction(nameof(PostAtivoFinanceiro), new { id = result.Id }, result);
        }

        //[HttpGet]
        //public async Task<Results<Ok<AtivoFinanceiroViewModel>, NotFound>> GetAtivoFinanceiro([FromQuery] string codigoNegociacao) {
        //    logger.LogInformation("Buscando ativo financeiro, codigo={}", codigoNegociacao);

        //    try {
        //        var result = await ativoFinanceiroQuery.ObterAtivoPorCodigoNegociacao(codigoNegociacao);
        //        logger.LogInformation("Sucesso ao buscar ativo financeiro, codigo={}", codigoNegociacao);
        //        return TypedResults.Ok(result);
        //    }
        //    catch {
        //        logger.LogError("Nao foi possivel encontrar ativo financeiro, codigo={}", codigoNegociacao);
        //        return TypedResults.NotFound();
        //    }
        //}
    }
}
