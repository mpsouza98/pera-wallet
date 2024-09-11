using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PeraInvest.API.Queries;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;

namespace PeraInvest.API.Controllers {

    [Route("/api/carteiras")]
    [ApiController]
    public class CarteiraController : ControllerBase {
        private readonly CarteiraContext context;
        private readonly IAtivoFinanceiroRepository ativoFinanceiroRepository;

        public CarteiraController(CarteiraContext carteiraContext, IAtivoFinanceiroRepository ativoRepository) {
            context = carteiraContext ?? throw new ArgumentNullException(nameof(context));
            ativoFinanceiroRepository = ativoRepository ?? throw new ArgumentNullException(nameof(ativoFinanceiroRepository));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Carteira>> GetCarteira(string id) {
        //    var carteira = await _context.Carteiras.FindAsync(id);

        //    if (carteira == null) {
        //        return NotFound();
        //    }

        //    return carteira;
        //}

        [HttpPost]
        public async Task<ActionResult<Carteira>> PostCarteira() {
            Carteira carteira = new(DateTime.Now);

            AtivoFinanceiro ativo = ativoFinanceiroRepository.ObterAtivo("string").Result;

            OperacaoAtivoCarteira operacao = new(
                ativo.Id,
                carteira.Id,
                9000.36m,
                9000.36m,
                DateTime.Now,
                DateTime.Now
            );

            carteira.AdicionaOperacaoCarteira(operacao);

            context.Add(carteira);

            var result = await context.SaveChangesAsync();

            return Ok(carteira);
        }
    }
}
