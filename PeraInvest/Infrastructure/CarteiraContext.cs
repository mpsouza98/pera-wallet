using Microsoft.EntityFrameworkCore;
using PeraInvest.Domain.CarteiraAggregate;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Infrastructure;

public partial class CarteiraContext : DbContext, IUnitOfWork {

    public CarteiraContext(DbContextOptions<CarteiraContext> options)
        : base(options) {
    }

    public virtual DbSet<AtivoFinanceiro> AtivosFinanceiro { get; set; }
    public virtual DbSet<OperacaoAtivoCarteira> OperacaoAtivoCarteiras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<AtivoFinanceiro>(entity => {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ativos_financeiro");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.ClasseAtivo).HasColumnName("classe_ativo");
            entity.Property(e => e.CodigoNegociacao)
                .HasMaxLength(40)
                .HasColumnName("codigo_negociacao");
            entity.Property(e => e.DataEmissao)
                .HasColumnType("datetime")
                .HasColumnName("data_emissao");
            entity.Property(e => e.DataVencimento)
                .HasColumnType("datetime")
                .HasColumnName("data_vencimento");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.Emissor)
                .HasMaxLength(255)
                .HasColumnName("emissor");
            entity.Property(e => e.Index)
                .HasPrecision(10)
                .HasColumnName("idx");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Status).HasColumnName("status");
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
