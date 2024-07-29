namespace PeraInvest.Domain.CarteiraAggregate.Repository {
    public interface IAtivoFinanceiroRepository : IRepository<AtivoFinanceiro> {

        AtivoFinanceiro CriarAtivo(AtivoFinanceiro ativoFinanceiro);
        AtivoFinanceiro ObterAtivo(string id);
        AtivoFinanceiro AtualizarAtivo(AtivoFinanceiro ativoFinanceiro);
        void DeletarAtivo(AtivoFinanceiro ativoFinanceiro);
    }
}
