namespace PeraInvest.Domain.models {
    public class Carteira {
        private int id { get; }
        private int idUsuario { get; set; }
        private DateTime criadoEm { get; set; }
        private List<AtivosFinanceiros> ativos { get; set; }
    }
}
