using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.API.Queries {
    public interface IOperacoesCarteiraQuery {
        Task<List<OperacaoAtivoCarteira>> ObterBlocoOperacoes(int size, int offset);
    }
}
