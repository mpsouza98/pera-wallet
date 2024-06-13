using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.models;

namespace PeraInvest.Adapters.Persistence.Context {
    public class AtivosFinanceirosContext : DbContext {

        public AtivosFinanceirosContext(DbContextOptions options) : base(options) {

        }

        public DbSet<AtivosFinanceiros> AtivosFinanceiros { get; set; }
    }
}
