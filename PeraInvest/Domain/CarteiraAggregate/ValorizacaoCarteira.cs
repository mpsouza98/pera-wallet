namespace PeraInvest.Domain.CarteiraAggregate
{
    public class ValorizacaoCarteira
    {
        public string CarteiraId { get; }
        public decimal TotalInvestido { get; set; }
        public decimal Rendimento { get; set; }
        public decimal Rentabilidade { get; set; }
        public DateTime DataValorizacao { get; set; }

        public ValorizacaoCarteira(string carteiraId, decimal totalInvestido, decimal rendimento, decimal rentabilidade, DateTime dataValorizacao) =>
            (CarteiraId, TotalInvestido, Rendimento, Rentabilidade, DataValorizacao) = (carteiraId, totalInvestido, rendimento, rentabilidade, dataValorizacao);
    }
}
