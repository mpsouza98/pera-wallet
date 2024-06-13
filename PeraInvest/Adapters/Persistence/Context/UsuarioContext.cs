using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Persistence.Context {
    public class UsuarioContext : DbContext {

        static readonly string connectionString = "Server=localhost:3306;User ID=YOURUSERID;Password=YOURPASSWORD;Database=perainvest";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
