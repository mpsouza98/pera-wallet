using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate {
    public class Carteira: IAggregateRoot {
        public string Id { get; }
        public string UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
        public List<OperacaoAtivoCarteira> AtivosCarteira { get; set; }

        public Carteira(string id, string usuarioId, DateTime criadoEm, List<OperacaoAtivoCarteira> ativosCarteira) =>
            (Id, UsuarioId, CriadoEm, AtivosCarteira) = (id, usuarioId, criadoEm, ativosCarteira);

        public decimal CalculaValorInvestido() {
            decimal valorTotal = 0;
            foreach (OperacaoAtivoCarteira ativoCarteira in AtivosCarteira) {
                valorTotal += ativoCarteira.ValorInvestido;
            }
            return valorTotal;
        }

        public decimal CalculaValorAcumulado() {
            decimal valorTotal = 0;
            foreach (OperacaoAtivoCarteira ativoCarteira in AtivosCarteira) {
                valorTotal += ativoCarteira.ValorAcumulado;
            }
            return valorTotal;
        }

        public decimal CalculaRendimentoCarteira() {
            decimal investido = CalculaValorAcumulado();
            decimal acumulado = CalculaValorInvestido();

            return investido - acumulado;
        }

        public decimal CalculoRentabilidade() {
            decimal rendimento = CalculaRendimentoCarteira();
            decimal investido = CalculaValorInvestido();

            return (rendimento / investido) * 100;
        }

        public OperacaoAtivoCarteira AdicionaOperacaoAtivoCarteira(OperacaoAtivoCarteira ativoCarteira) {
            var OperacaoAtivoCarteiraExistente = AtivosCarteira.FirstOrDefault(a => a.IsEqualTo(ativoCarteira.Ativo, ativoCarteira.DataCompra));

            if (OperacaoAtivoCarteiraExistente != null) {
                OperacaoAtivoCarteiraExistente.ValorInvestido += ativoCarteira.ValorInvestido;
                return OperacaoAtivoCarteiraExistente;
            }

            AtivosCarteira.Add(ativoCarteira);
            return ativoCarteira;
        }
    }
}
