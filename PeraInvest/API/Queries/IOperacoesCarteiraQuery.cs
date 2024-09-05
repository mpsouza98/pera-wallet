using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.API.Queries {
    interface IOperacoesCarteiraQuery {
        Task<List<OperacaoAtivoCarteira>> ObterBlocoOperacoes(int size, int offset);
    }
}
