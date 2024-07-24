
using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.Domain.Ports {
    interface AtivoFinanceiroPort {
        AtivoFinanceiro CriarAtivoFinanceiro(AtivoFinanceiro ativoFinanceiro);
        AtivoFinanceiro ObterAtivoFinanceiro(string id);
        AtivoFinanceiro AtualizarAtivoFinanceiro(AtivoFinanceiro ativoFinanceiro);
        void DeletarAtivoFinanceiro(string id);
    }
}
