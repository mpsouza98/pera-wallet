using MediatR;
using PeraInvest.Domain.CarteiraAggregate;
using System.Runtime.Serialization;
using static PeraInvest.Domain.CarteiraAggregate.AtivoFinanceiro;

namespace PeraInvest.API.Commands {

    [DataContract]
    public class CriarAtivoCommand : IRequest<AtivoFinanceiro> {

        [DataMember]
        public string Nome { get; private set; }

        [DataMember]
        public string Descricao { get; private set; }

        [DataMember]
        public string CodigoNegociacao { get; private set; }

        [DataMember]
        public decimal? Index { get; private set; }

        [DataMember]
        public ClassesAtivo ClasseAtivo { get; private set; }

        [DataMember]
        public DateTime? DataVencimento { get; private set; }

        [DataMember]
        public DateTime DataEmissao { get; private set; }

        [DataMember]
        public string Emissor { get; private set; }

        [DataMember]
        public Boolean Status { get; private set; }

        public CriarAtivoCommand(string nome, string descricao, string codigoNegociacao, decimal? index, ClassesAtivo classeAtivo, DateTime? dataVencimento, DateTime dataEmissao, string emissor, bool status) {
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
    }
}
