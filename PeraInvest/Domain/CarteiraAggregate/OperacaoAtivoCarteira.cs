using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate {
    public class OperacaoAtivoCarteira: Entity, IAggregateRoot {
        public byte[] AtivoId { get; set; } = null!;
        public byte[] CarteiraId { get; set; } = null!;
        public decimal ValorInvestido { get; set; }
        public decimal ValorAcumulado { get; set; }
        public DateTime DataValorizacao { get; set; }
        public DateTime DataCompra { get; set; }

        public virtual AtivoFinanceiro Ativo { get; set; } = null!;
        public virtual Carteira Carteira { get; set; } = null!;

        public OperacaoAtivoCarteira(byte[] ativoId, byte[] carteiraId, decimal valorInvestido, decimal valorAcumulado, DateTime dataValorizacao, DateTime dataCompra) {
            Id = Guid.NewGuid().ToByteArray();
            AtivoId = ativoId;
            CarteiraId = carteiraId;
            ValorInvestido = valorInvestido;
            ValorAcumulado = valorAcumulado;
            DataValorizacao = dataValorizacao;
            DataCompra = dataCompra;
        }

        public bool IsEqualTo(AtivoFinanceiro ativoFinanceiro, DateTime dataCompra) {
            return Ativo.IsEqualTo(ativoFinanceiro)
                && DataCompra.Equals(dataCompra);
        }
    }
}