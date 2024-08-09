//using Infrastructure;
//using PeraInvest.Domain.CarteiraAggregate;
//using PeraInvest.Domain.CarteiraAggregate.Repository;
//using PeraInvest.Domain.SeedWork;

//namespace PeraInvest.Infrastructure.Repositories {
//    public class CarteiraRepository : ICarteiraRepository {
//        private readonly CarteiraContext context;

//        public IUnitOfWork UnitOfWork {
//            get {
//                return context;
//            }
//        }

//        public CarteiraRepository(CarteiraContext carteiraContext) {
//            context = carteiraContext ?? throw new ArgumentNullException(nameof(carteiraContext));
//        }

//        public OperacaoAtivoCarteira AdicionarAtivo(Carteira carteira, OperacaoAtivoCarteira ativoCarteira) {
//            throw new NotImplementedException();
//        }

//        public Carteira CriarCarteira(string usuarioId, List<OperacaoAtivoCarteira> ativosCarteira) {
//            throw new NotImplementedException();
//        }

//        public void DeletarAtivo(Carteira carteira, OperacaoAtivoCarteira ativoCarteira) {
//            throw new NotImplementedException();
//        }

//        public void Dispose() {
//            throw new NotImplementedException();
//        }

//        public Carteira ObterCarteira(string id) {
//            throw new NotImplementedException();
//        }
//    }
//}
