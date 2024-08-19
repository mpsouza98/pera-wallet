using PeraInvest.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace PeraInvest.Domain.CarteiraAggregate {
    public class Carteira: Entity, IAggregateRoot {
        
        public byte[]? UsuarioId { get; set; } = null!;
        public DateTime CriadoEm { get; }

        // DDD Patterns comment
        // Using a private collection field, better for DDD Aggregate's encapsulation
        // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
        // but only through the method OrderAggregateRoot.AddOrderItem() which includes behavior.
        private readonly List<OperacaoAtivoCarteira> _operacoesAtivoCarteira;
        public IReadOnlyCollection<OperacaoAtivoCarteira> OperacoesAtivoCarteira => _operacoesAtivoCarteira.AsReadOnly();

        // EF Core constructor https://stackoverflow.com/questions/55749717/entity-framework-cannot-bind-value-object-in-entity-constructor
        protected Carteira() => _operacoesAtivoCarteira = [];

        public Carteira(DateTime criadoEm): this() {
            Id = Guid.NewGuid().ToByteArray();
            CriadoEm = criadoEm;
        }

        public decimal CalculaValorInvestido() {
            decimal valorTotal = 0;
            foreach (OperacaoAtivoCarteira ativoCarteira in OperacoesAtivoCarteira) {
                valorTotal += ativoCarteira.ValorInvestido;
            }
            return valorTotal;
        }

        public decimal CalculaValorAcumulado() {
            decimal valorTotal = 0;
            foreach (OperacaoAtivoCarteira ativoCarteira in OperacoesAtivoCarteira) {
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

        public OperacaoAtivoCarteira AdicionaOperacaoCarteira(OperacaoAtivoCarteira ativoCarteira) {
            var OperacaoAtivoCarteiraExistente = _operacoesAtivoCarteira.FirstOrDefault(a => a.IsEqualTo(ativoCarteira.Ativo, ativoCarteira.DataCompra));

            if (OperacaoAtivoCarteiraExistente != null) {
                OperacaoAtivoCarteiraExistente.ValorInvestido += ativoCarteira.ValorInvestido;
                return OperacaoAtivoCarteiraExistente;
            }

            _operacoesAtivoCarteira.Add(ativoCarteira);
            return ativoCarteira;
        }
    }
}
