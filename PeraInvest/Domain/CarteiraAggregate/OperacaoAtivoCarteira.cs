namespace PeraInvest.Domain.CarteiraAggregate
{
    public class OperacaoAtivoCarteira
    {
        public string Id { get; }
        public AtivoFinanceiro Ativo { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal ValorAcumulado { get; set; }
        public DateTime DataValorizacao { get; set; }
        public DateTime DataCompra {  get; set; }

        public OperacaoAtivoCarteira(string id, AtivoFinanceiro ativo, decimal valorInvestido, decimal valorAcumulado, DateTime dataValorizacao, DateTime dataCompra) {
            Id = id;
            Ativo = ativo;
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
