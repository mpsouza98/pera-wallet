namespace PeraInvest.Domain.CarteiraAggregate.Repository {
    public interface IAtivoFinanceiroRepository : IRepository<AtivoFinanceiro> {

        AtivoFinanceiro CriarAtivo(AtivoFinanceiro ativoFinanceiro);
        Task<AtivoFinanceiro> ObterAtivo(string codigo);
        Task<bool> IsAtivoExistente(string codigo);
        AtivoFinanceiro AtualizarAtivo(AtivoFinanceiro ativoFinanceiro);
        void DeletarAtivo(AtivoFinanceiro ativoFinanceiro);

    }
}
