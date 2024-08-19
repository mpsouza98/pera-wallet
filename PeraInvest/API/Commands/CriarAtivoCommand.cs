using MediatR;
using PeraInvest.API.Commands.Handlers;
using System.Runtime.Serialization;
using static PeraInvest.Domain.CarteiraAggregate.AtivoFinanceiro;

namespace PeraInvest.API.Commands {

    [DataContract]
    public class CriarAtivoCommand : IRequest<CriarAtivoFinanceiroResponse> {

        [DataMember(EmitDefaultValue = false)]
        public string Nome { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public string Descricao { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoNegociacao { get; private set; }

        [DataMember(IsRequired = false)]
        public decimal? Index { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public ClassesAtivo ClasseAtivo { get; private set; }

        [DataMember(IsRequired = false)]
        public DateTime? DataVencimento { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime DataEmissao { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public string Emissor { get; private set; }

        [DataMember(EmitDefaultValue = false)]
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
