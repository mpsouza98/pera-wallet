using System.Security.Cryptography.X509Certificates;

namespace PeraInvest.Domain.CarteiraAggregate
{
    public class AtivoCarteira
    {
        public string Id { get; }
        public AtivoFinanceiro Ativo { get; set; }
        public decimal ValorInvestido { get; set; }
        public DateTime DataCompra {  get; set; }

        public AtivoCarteira(string id, AtivoFinanceiro ativo, decimal valorInvestido) =>
            (Id, Ativo, ValorInvestido) = (id, ativo, valorInvestido);

        public bool IsEqualTo(AtivoFinanceiro ativoFinanceiro, DateTime dataCompra) {
            return Ativo.IsEqualTo(ativoFinanceiro) 
                && DataCompra.Equals(dataCompra);
        }
    }
}
