namespace PeraInvest.Domain.CarteiraAggregate.Repository {
    public interface ICarteiraRepository : IRepository<Carteira> {

        AtivoCarteira AdicionarAtivo(Carteira carteira, AtivoCarteira ativoCarteira);
        void DeletarAtivo(Carteira carteira, AtivoCarteira ativoCarteira);
        Carteira CriarCarteira(string usuarioId, List<AtivoCarteira> ativosCarteira);
        Carteira ObterCarteira(string id);
    }
}
