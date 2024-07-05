namespace PeraInvest.Domain.models {
    class AtivoFinanceiro {
        public string Id { get; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoNegociacao { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataAtualizacaoValor { get; set; }
        public ClassesAtivo ClasseAtivo { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Emissor { get; set; }
        public bool Status { get; set; }


        public AtivoFinanceiro(string id, string nome, string descricao, string codigoNegociacao, decimal valorAtual, DateTime dataAtualizacaoValor, ClassesAtivo classesAtivo, DateTime dataVencimento, DateTime dataEmissao, string emissor, bool statusAtivo) {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            CodigoNegociacao = codigoNegociacao;
            ValorAtual = valorAtual;
            DataAtualizacaoValor = dataAtualizacaoValor;
            ClasseAtivo = classesAtivo;
            DataVencimento = dataVencimento;
            DataEmissao = dataEmissao;
            Emissor = emissor;
            Status = statusAtivo;
        }

        public enum ClassesAtivo { ACAO, CRIPTOMOEDA, DEBENTURE, FUNDO_IMOBILIARIO, MOEDA, PREVIDENCIA, RENDA_FIXA_PREFIXADA, RENDA_FIXA_POS_FIXADA, TESOURO_DIRETO }
    }
}
