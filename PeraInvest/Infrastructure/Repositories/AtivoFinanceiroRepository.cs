using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Infrastructure.Repositories {
    public class AtivoFinanceiroRepository : IAtivoFinanceiroRepository {
        private readonly AtivoFinanceiroContext _ativoFinanceiroContext;

        public IUnitOfWork UnitOfWork {
            get {
                return _ativoFinanceiroContext;
            }
        }

        public AtivoFinanceiroRepository(AtivoFinanceiroContext ativoFinanceiroContext) {
            _ativoFinanceiroContext = ativoFinanceiroContext;
        }

        public AtivoFinanceiro AtualizarAtivo(AtivoFinanceiro ativoFinanceiro) {
            throw new NotImplementedException();
        }

        public AtivoFinanceiro CriarAtivo(AtivoFinanceiro ativoFinanceiro) {
            return _ativoFinanceiroContext.Add(ativoFinanceiro).Entity;
        }

        public void DeletarAtivo(AtivoFinanceiro ativoFinanceiro) {
            _ativoFinanceiroContext.Remove(ativoFinanceiro);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public AtivoFinanceiro ObterAtivo(string id) {
            throw new NotImplementedException();
        }
    }
}
