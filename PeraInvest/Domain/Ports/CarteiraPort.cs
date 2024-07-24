using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.Domain.Ports
{
    interface ICarteiraPort {
        AtivoCarteira AdicionarAtivoCarteira(Carteira carteira, AtivoCarteira ativoCarteira);
        void RemoverAtivoCarteira(Carteira carteira, AtivoCarteira ativoCarteira);
        Carteira CriarCarteira(string usuarioId, List<AtivoCarteira> ativosCarteira);
        Carteira ObterCarteira(string id);
    }
}
