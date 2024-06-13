namespace PeraInvest.Domain.models {
    public class Usuario {
        private string id { get; }
        private string email { get; set; }
        private string senha { get; set; }

        Usuario(string email, string senha) {
            this.email = email;
            this.senha = senha;
        }
    }
}
