namespace PeraInvest.Domain.models {
    public class AtivosFinanceiros {
        private string id { get; set; }
        private string nome { get; set; }
        private string descricao { get; set; }
        private string codigoNegociacao { get; set; }
        private Decimal valorAtual { get; set; }
        private ClassesAtivo classesAtivo { get; set; }
        private DateTime dataVencimento { get; set; }
        private DateTime dataEmissao { get; set; }
        private string emissor { get; set; }
        private StatusAtivo statusAtivo { get; set; }

        public AtivosFinanceiros(string id, string nome, string descricao, string codigoNegociacao, decimal valorAtual, ClassesAtivo classesAtivo, DateTime dataVencimento, DateTime dataEmissao, string emissor, StatusAtivo statusAtivo) {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.codigoNegociacao = codigoNegociacao;
            this.valorAtual = valorAtual;
            this.classesAtivo = classesAtivo;
            this.dataVencimento = dataVencimento;
            this.dataEmissao = dataEmissao;
            this.emissor = emissor;
            this.statusAtivo = statusAtivo;
        }

        public enum StatusAtivo { ATIVO, INATIVO }
        public enum ClassesAtivo { ACAO, CRIPTOMOEDA, DEBENTURE, FUNDO_IMOBILIARIO, MOEDA, PREVIDENCIA, RENDA_FIXA_PREFIXADA, RENDA_FIXA_POS_FIXADA, TESOURO_DIRETO }
    }
}
