using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Persistence.Context {
    public class UsuarioContext : DbContext {


        public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
