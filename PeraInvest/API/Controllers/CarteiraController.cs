using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using PeraInvest.Domain.SeedWork;
using PeraInvest.Infrastructure.Repositories;

namespace PeraInvest.API.Controllers {
    [Route("/api/carteiras")]
    [ApiController]
    public class CarteiraController : ControllerBase {
        private readonly CarteiraContext context;
        private readonly IAtivoFinanceiroRepository ativoFinanceiroRepository;

        public CarteiraController(CarteiraContext carteiraContext, IAtivoFinanceiroRepository ativoRepository) {
            context = carteiraContext;
            ativoFinanceiroRepository = ativoRepository
                ?? throw new ArgumentNullException(nameof(ativoFinanceiroRepository));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Carteira>>> GetCarteiras() {
        //    return await _context.Carteiras.ToListAsync();
        //}

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

            AtivoFinanceiro ativo = ativoFinanceiroRepository.ObterAtivo("string3").Result;

            OperacaoAtivoCarteira operacao = new(
                ativo.Id,
                carteira.Id,
                9000.36m,
                9000.36m,
                DateTime.Now,
                DateTime.Now
            );

            carteira.AdicionaOperacaoCarteira( operacao );

            context.Add(carteira);

            var result = await context.SaveChangesAsync();

            return Ok(carteira);
        }
    }
}
