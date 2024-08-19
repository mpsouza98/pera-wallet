using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.SeedWork;

namespace Infrastructure;

public partial class CarteiraContext : DbContext, IUnitOfWork {
    public CarteiraContext() {
    }

    public CarteiraContext(DbContextOptions<CarteiraContext> options)
        : base(options) {
    }

    public virtual DbSet<AtivoFinanceiro> AtivosFinanceiro { get; set; }

    public virtual DbSet<Carteira> Carteiras { get; set; }

    public virtual DbSet<OperacaoAtivoCarteira> OperacoesCarteira { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Carteira>(entity => {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carteiras");

            entity.HasIndex(e => e.UsuarioId, "usuario_id").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.CriadoEm)
                .HasColumnType("datetime")
                .HasColumnName("criado_em");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("usuario_id");
        });

        modelBuilder.Entity<OperacaoAtivoCarteira>(entity => {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("operacoes_carteira");

            entity.HasIndex(e => e.AtivoId, "ativo_id");

            entity.HasIndex(e => e.CarteiraId, "carteira_id");

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.AtivoId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("ativo_id");
            entity.Property(e => e.CarteiraId)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("carteira_id");
            entity.Property(e => e.DataCompra)
                .HasColumnType("datetime")
                .HasColumnName("data_compra");
            entity.Property(e => e.DataValorizacao)
                .HasColumnType("datetime")
                .HasColumnName("data_valorizacao");
            entity.Property(e => e.ValorAcumulado)
                .HasPrecision(10, 2)
                .HasColumnName("valor_acumulado");
            entity.Property(e => e.ValorInvestido)
                .HasPrecision(10, 2)
                .HasColumnName("valor_investido");

            entity.HasOne(d => d.Ativo).WithMany(p => p.OperacoesAtivoCarteira)
                .HasForeignKey(d => d.AtivoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("operacoes_carteira_ibfk_1");
        });
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        //await mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

}
