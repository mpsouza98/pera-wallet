using System.Text.Json;

namespace PeraInvest.API.Clients {

    public interface IUsuarioClient {
        Task<Usuario> getUser();
    }
    public class UsuarioClient : IUsuarioClient {
        private readonly HttpClient client;
        private readonly ILogger<UsuarioClient> logger;

        public UsuarioClient(HttpClient client, ILogger<UsuarioClient> logger) {
            this.client = client;
            this.logger = logger;
        }

        public async Task<Usuario> getUser() {
            var response = await client.GetAsync("?username=mpsouza");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            logger.LogInformation("Usuario: {}", jsonResponse);

            var usuario = JsonSerializer.Deserialize<List<Usuario>>(jsonResponse) ?? throw new Exception("Deu caquinha!");

            return usuario.First();
        }
    }

    public class Access {
        public bool manageGroupMembership { get; set; }
        public bool view { get; set; }
        public bool mapRoles { get; set; }
        public bool impersonate { get; set; }
        public bool manage { get; set; }
    }

    public class Usuario {
        public string id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool emailVerified { get; set; }
        public long createdTimestamp { get; set; }
        public bool enabled { get; set; }
        public bool totp { get; set; }
        public List<object> disableableCredentialTypes { get; set; }
        public List<object> requiredActions { get; set; }
        public int notBefore { get; set; }
        public Access access { get; set; }
    }
}
