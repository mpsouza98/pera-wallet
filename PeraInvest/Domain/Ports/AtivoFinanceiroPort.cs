using PeraInvest.Domain.models;

namespace PeraInvest.Domain.Ports {
    interface AtivoFinanceiroPort {
        void CriarAtivoFinanceiro(AtivoFinanceiro ativoFinanceiro);
    }
}
