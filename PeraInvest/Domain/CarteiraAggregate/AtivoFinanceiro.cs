using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate
{
    public class AtivoFinanceiro : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoNegociacao { get; set; }
        public decimal? Index { get; set; }
        public ClassesAtivo ClasseAtivo { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string? Emissor { get; set; }
        public Boolean Status { get; set; }
        public ICollection<OperacaoAtivoCarteira>? OperacoesAtivoCarteira { get; } = [];

        public enum ClassesAtivo { ACAO, CRIPTOMOEDA, DEBENTURE, FUNDO_IMOBILIARIO, MOEDA, PREVIDENCIA, RENDA_FIXA_PREFIXADA, RENDA_FIXA_POS_FIXADA, TESOURO_DIRETO }

        public AtivoFinanceiro(string nome, string descricao, string codigoNegociacao, decimal? index, ClassesAtivo classeAtivo, DateTime? dataVencimento, DateTime? dataEmissao, string? emissor, bool status) {
            Id = Guid.NewGuid().ToByteArray();
            Nome = nome;
            Descricao = descricao;
            CodigoNegociacao = codigoNegociacao;
            Index = index;
            ClasseAtivo = classeAtivo;
            DataVencimento = dataVencimento;
            DataEmissao = dataEmissao;
            Emissor = emissor;
            Status = status;
        }

        public bool IsEqualTo(AtivoFinanceiro ativoFinanceiro) => ativoFinanceiro.Id == Id;
    }
}