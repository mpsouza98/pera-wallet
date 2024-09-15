using MediatR;
using Microsoft.EntityFrameworkCore;
using PeraInvest.API.Batch.Models;
using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Infrastructure;

public class BatchContext : DbContext, IUnitOfWork {
    public BatchContext(DbContextOptions<BatchContext> options, IMediator mediator)
        : base(options) {
    }

    public virtual DbSet<BlocoProcessamento> BlocosProcessamento { get; set; }
    public virtual DbSet<Rotina> Rotina { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<BlocoProcessamento>(entity => {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("blocos_processamento");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.RotinaId)
                .HasColumnName("rotina_id")
                .HasMaxLength(36)
                .IsFixedLength();
            entity.Property(e => e.Estado)
                .HasColumnName("estado");
            entity.Property(e => e.TamanhoBloco)
                .HasColumnName("size");
            entity.Property(e => e.Offset)
                .HasColumnName("offset");
        });

        modelBuilder.Entity<Rotina>(entity => {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rotina");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsFixedLength();
            entity.Property(e => e.Nome)
                .HasColumnName("nome")
                .HasMaxLength(60);
            entity.Property(e => e.DataInicioProcessamento)
                .HasColumnName("data_inicio_processamento");
            entity.Property(e => e.DataFimProcessamento)
                .HasColumnName("data_fim_processamento");
            entity.Property(e => e.EstadoExecucao)
                .HasColumnName("estado");
        });
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
        var result = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
