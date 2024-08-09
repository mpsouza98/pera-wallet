
using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.API.Queries.ViewModels {
    public record CarteiraViewModel {
        public string Id { get; init; }
        public string UsuarioId { get; init; }
        public DateTime CriadoEm { get; init; }
        public List<OperacaoAtivoCarteiraViewModel> AtivosCarteira { get; init; }
        public decimal ValorInvestido { get; init; }
        public decimal RendimentoTotalCarteira { get; init; }
    }

    public record OperacaoAtivoCarteiraViewModel {
        public string Id { get; init; }
        public AtivoFinanceiro Ativo { get; init; }
        public decimal ValorInvestido { get; init; }
        public DateTime DataCompra { get; init; }
    }

    public record ValorizacaoOperacaoAtivoCarteiraViewModel {
        public string CarteiraId { get; init; }
        public decimal TotalInvestido { get; init; }
        public decimal Rendimento { get; init; }
        public decimal Rentabilidade { get; init; }
        public DateTime DataValorizacao { get; init; }
    }
}
