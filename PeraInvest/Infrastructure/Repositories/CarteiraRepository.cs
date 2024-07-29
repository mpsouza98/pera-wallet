using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Infrastructure.Repositories {
    public class CarteiraRepository : ICarteiraRepository {
        private readonly CarteiraContext _carteiraContext;

        public IUnitOfWork UnitOfWork {
            get {
                return _carteiraContext;
            }
        }

        public CarteiraRepository(CarteiraContext carteiraContext) {
            _carteiraContext = carteiraContext ?? throw new ArgumentNullException(nameof(carteiraContext));
        }

        public AtivoCarteira AdicionarAtivo(Carteira carteira, AtivoCarteira ativoCarteira) {
            throw new NotImplementedException();
        }

        public Carteira CriarCarteira(string usuarioId, List<AtivoCarteira> ativosCarteira) {
            throw new NotImplementedException();
        }

        public void DeletarAtivo(Carteira carteira, AtivoCarteira ativoCarteira) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public Carteira ObterCarteira(string id) {
            throw new NotImplementedException();
        }
    }
}
