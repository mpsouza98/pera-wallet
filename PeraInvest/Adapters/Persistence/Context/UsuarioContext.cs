using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Persistence.Context {
    public class UsuarioContext : DbContext {

        public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Usuario>();
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
