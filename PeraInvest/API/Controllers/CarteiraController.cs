using Microsoft.AspNetCore.Mvc;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Infrastructure;

namespace PeraInvest.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarteiraController : ControllerBase {
        private readonly CarteiraContext _context;

        public CarteiraController(CarteiraContext context) {
            _context = context;
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
    }
}
