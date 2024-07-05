using PeraInvest.Domain.models;

namespace PeraInvest.Domain.Models {
    class AtivoCarteira {
        public string Id { get; }
        public AtivoFinanceiro Ativo { get; set; }
        public decimal Quantidade { get; set; }

        public AtivoCarteira(string id, AtivoFinanceiro ativo, decimal quantidade) {
            Id = id;
            Ativo = ativo;
            Quantidade = quantidade;
        }
    }
}
