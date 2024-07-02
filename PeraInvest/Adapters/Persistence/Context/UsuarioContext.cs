//using Microsoft.EntityFrameworkCore;
//using NUlid;
//using PeraInvest.Domain.models;
//using System.Xml;

//namespace PeraInvest.Adapters.Persistence.Context {
//    public class UsuarioContext : DbContext {

//        public UsuarioContext(DbContextOptions<UsuarioContext> options)
//        : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder) {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Usuario>()
//                .Property(e => e.id)
//                .IsRequired()
//                .HasMaxLength(26);
//        }

//        public DbSet<Usuario> Usuarios { get; set; }
//    }
//}
