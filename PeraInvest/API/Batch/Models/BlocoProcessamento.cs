using PeraInvest.Domain.SeedWork;
using static PeraInvest.API.Batch.Models.Rotina;

namespace PeraInvest.API.Batch.Models {
    public class BlocoProcessamento: IAggregateRoot {
        public int Id { get; set; }
        public string RotinaId{ get; set; }
        public Estado Estado {  get; set; }
        public int TamanhoBloco { get; set; }
        public int Offset { get; set; }

        public BlocoProcessamento(int id, string rotinaId, Estado estado, int tamanhoBloco, int offset) {
            Id = id;
            RotinaId= rotinaId;
            Estado = estado;
            TamanhoBloco = tamanhoBloco;
            Offset = offset;
        }
    }
}
