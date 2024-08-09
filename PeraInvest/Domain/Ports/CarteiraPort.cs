using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.Domain.Ports
{
    interface ICarteiraPort {
        OperacaoAtivoCarteira AdicionarOperacaoAtivoCarteira(Carteira carteira, OperacaoAtivoCarteira ativoCarteira);
        void RemoverOperacaoAtivoCarteira(Carteira carteira, OperacaoAtivoCarteira ativoCarteira);
        Carteira CriarCarteira(string usuarioId, List<OperacaoAtivoCarteira> ativosCarteira);
        Carteira ObterCarteira(string id);
    }
}
