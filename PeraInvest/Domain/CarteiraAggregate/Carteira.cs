using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate {
    public class Carteira: IAggregateRoot {
        public string Id { get; }
        public string UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
        public List<AtivoCarteira> AtivosCarteira { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal RendimentoTotalCarteira { get; set; }

        public Carteira(string id, string usuarioId, DateTime criadoEm, List<AtivoCarteira> ativosCarteira, decimal valorTotal, decimal rendimentoTotalCarteira) =>
            (Id, UsuarioId, CriadoEm, AtivosCarteira, ValorInvestido, RendimentoTotalCarteira) = (id, usuarioId, criadoEm, ativosCarteira, valorTotal, rendimentoTotalCarteira);

        public void CalculaValorInvestido() {
            decimal valorTotal = 0;
            foreach (AtivoCarteira ativoCarteira in AtivosCarteira) {
                valorTotal += ativoCarteira.ValorInvestido;
            }
            ValorInvestido = valorTotal;
        }

        public AtivoCarteira AdicionaAtivoCarteira(AtivoCarteira ativoCarteira) {
            var AtivoCarteiraExistente = AtivosCarteira.FirstOrDefault(a => a.IsEqualTo(ativoCarteira.Ativo, ativoCarteira.DataCompra));

            if (AtivoCarteiraExistente != null) {
                AtivoCarteiraExistente.ValorInvestido += ativoCarteira.ValorInvestido;
                return AtivoCarteiraExistente;
            }

            AtivosCarteira.Add(ativoCarteira);
            return ativoCarteira;
        }
    }
}
