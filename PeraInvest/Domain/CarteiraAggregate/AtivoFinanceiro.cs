using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate
{
    public class AtivoFinanceiro : IAggregateRoot
    {
        public string Id { get; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoNegociacao { get; set; }
        public decimal? Index { get; set; }
        public ClassesAtivo ClasseAtivo { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Emissor { get; set; }
        public Boolean Status { get; set; }

        public enum ClassesAtivo { ACAO, CRIPTOMOEDA, DEBENTURE, FUNDO_IMOBILIARIO, MOEDA, PREVIDENCIA, RENDA_FIXA_PREFIXADA, RENDA_FIXA_POS_FIXADA, TESOURO_DIRETO }

        public AtivoFinanceiro(string id, string nome, string descricao, string codigoNegociacao, decimal? index, ClassesAtivo classeAtivo, DateTime? dataVencimento, DateTime dataEmissao, string emissor, Boolean status) =>
            (Id, Nome, Descricao, CodigoNegociacao, Index, ClasseAtivo, DataVencimento, DataEmissao, Emissor, Status) = (id, nome, descricao, codigoNegociacao, index, classeAtivo, dataVencimento, dataEmissao, emissor, status);

        public bool IsEqualTo(AtivoFinanceiro ativoFinanceiro) => ativoFinanceiro.Id == Id;
    }
}