namespace PeraInvest.API.Queries.ViewModels {
    public record AtivoFinanceiroViewModel {
        public string Id { get; init; }
        public string Nome { get; init; }
        public string Descricao { get; init; }
        public string CodigoNegociacao { get; init; }
        public decimal? Index { get; init; }
        public string ClasseAtivo { get; init; }
        public DateTime? DataVencimento { get; init; }
        public DateTime DataEmissao { get; init; }
        public string Emissor { get; init; }
        public string Status { get; init; }
    }
}
