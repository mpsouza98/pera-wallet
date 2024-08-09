using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.Ocsp;
using PeraInvest.API.Commands;
using PeraInvest.API.Queries;
using PeraInvest.API.Queries.ViewModels;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Infrastructure;

namespace PeraInvest.API.Controllers {

    [Route("/api/ativo_financeiro")]
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
        public async Task<Results<Ok<AtivoFinanceiro>, BadRequest<CriarAtivoCommand>>> PostAtivoFinanceiro([FromBody] CriarAtivoCommand criarAtivoCommand) {
            logger.LogInformation("Criando novo ativo financeiro");
            
            var result = await mediator.Send(criarAtivoCommand);
            logger.LogInformation("Sucesso no comando de criar ativo financeiro");

            return TypedResults.Ok(result);
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
