using Infrastructure;
using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.API.Queries {
    public interface IOperacoesCarteiraQuery {
        Task<List<OperacaoAtivoCarteira>> ObterBlocoOperacoes(int size, int offset);
        Task<int> ConsultaTotalOperacoes();
    }

    public class OperacoesCarteiraQuery : IOperacoesCarteiraQuery {
        private readonly CarteiraContext context;

        public OperacoesCarteiraQuery(CarteiraContext context) {
            this.context = context;
        }

        public async Task<List<OperacaoAtivoCarteira>> ObterBlocoOperacoes(int size, int offset) {
            var operacoes = await context.OperacoesCarteira
                .FromSql($"SELECT * FROM operacoes_carteira LIMIT {size} OFFSET {offset}")
                .ToListAsync();

            return operacoes;
        }

        public async Task<int> ConsultaTotalOperacoes() => await context.OperacoesCarteira.CountAsync();
    }
}
