using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.CarteiraAggregate.Exceptions;
using PeraInvest.Domain.CarteiraAggregate.Repository;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Infrastructure.Repositories {
    public class AtivoFinanceiroRepository : IAtivoFinanceiroRepository {
        private readonly AtivoFinanceiroContext context;

        public IUnitOfWork UnitOfWork {
            get {
                return context;
            }
        }

        public AtivoFinanceiroRepository(AtivoFinanceiroContext context) {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AtivoFinanceiro AtualizarAtivo(AtivoFinanceiro ativoFinanceiro) {
            throw new NotImplementedException();
        }

        public AtivoFinanceiro CriarAtivo(AtivoFinanceiro ativoFinanceiro) {
            return context.AtivosFinanceiro.Add(ativoFinanceiro).Entity;
        }

        public void DeletarAtivo(AtivoFinanceiro ativoFinanceiro) {
            context.Remove(ativoFinanceiro);
        }

        public async Task<AtivoFinanceiro> ObterAtivo(string codigo) {
            var result = await context.AtivosFinanceiro
                .Where(a => a.CodigoNegociacao == codigo)
                .SingleAsync() ?? throw new EntityNotFoundException($"AtivoFinanceiro nao encontrado! codigo={codigo}");

            return result;
        }

        public async Task<bool> IsAtivoExistente(string codigo) {
            var result = await context.AtivosFinanceiro
                .Where(a => a.CodigoNegociacao == codigo)
                .SingleAsync();

            return (result != null);
        }
    }
}
