using PeraInvest.Domain.models;
using PeraInvest.Domain.Models;

namespace PeraInvest.Domain.Ports {
    interface ICarteiraPort {
        AtivoCarteira AdicionarAtivoCarteira(Carteira carteira, AtivoFinanceiro ativo, decimal quantidade);
        void RemoverAtivoCarteira(Carteira carteira, AtivoCarteira ativoCarteira);

    }
}
