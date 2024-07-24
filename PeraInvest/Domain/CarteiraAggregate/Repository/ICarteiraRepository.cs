using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.Domain.CarteiraAggregate.Repository
{
    interface ICarteiraRepository
    {

        AtivoCarteira Add(Carteira carteira, AtivoCarteira ativoCarteira);
        void Delete(Carteira carteira, AtivoCarteira ativoCarteira);
        Carteira CriarCarteira(string usuarioId, List<AtivoCarteira> ativosCarteira);
        Carteira ObterCarteira(string id);
    }
}
