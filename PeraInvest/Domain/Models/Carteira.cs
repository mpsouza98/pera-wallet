using NUlid;
using PeraInvest.Domain.Models;

namespace PeraInvest.Domain.models {
    class Carteira {
        public int Id { get; }
        public string UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
        public List<AtivoCarteira> AtivosCarteira { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal RendimentoTotalCarteira { get; set; }

        public Carteira(int id, string usuario, DateTime criadoEm, List<AtivoCarteira> ativos) {
            Id = id;
            UsuarioId = usuario;
            CriadoEm = criadoEm;
            AtivosCarteira = ativos;
            ValorTotal = calculaValorTotal();
            RendimentoTotalCarteira = 0;
        }

        public decimal calculaValorTotal() {
            decimal valorTotal = 0;
            foreach (AtivoCarteira ativoCarteira in AtivosCarteira) {
                valorTotal += ativoCarteira.Ativo.ValorAtual * ativoCarteira.Quantidade;
            }
            return valorTotal;
        }
    }
}
