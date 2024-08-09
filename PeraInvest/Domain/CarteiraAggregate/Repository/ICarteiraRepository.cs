namespace PeraInvest.Domain.CarteiraAggregate.Repository {
    public interface ICarteiraRepository : IRepository<Carteira> {

        OperacaoAtivoCarteira AdicionarAtivo(Carteira carteira, OperacaoAtivoCarteira ativoCarteira);
        void DeletarAtivo(Carteira carteira, OperacaoAtivoCarteira ativoCarteira);
        Carteira CriarCarteira(string usuarioId, List<OperacaoAtivoCarteira> ativosCarteira);
        Carteira ObterCarteira(string id);
    }
}
