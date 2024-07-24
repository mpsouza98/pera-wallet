using System;
using System.Collections.Generic;
using Domain.CarteiraAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public partial class CarteiraContext : DbContext
{
    public CarteiraContext()
    {
    }

    public CarteiraContext(DbContextOptions<CarteiraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AtivosFinanceiro> AtivosFinanceiros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtivosFinanceiro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ativos_financeiro");

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.ClasseAtivo)
                .HasMaxLength(255)
                .HasColumnName("classe_ativo");
            entity.Property(e => e.CodigoNegociacao)
                .HasMaxLength(40)
                .HasColumnName("codigo_negociacao");
            entity.Property(e => e.CotacaoAtual)
                .HasPrecision(10)
                .HasColumnName("cotacao_atual");
            entity.Property(e => e.DataAtualizacaoCotacao)
                .HasColumnType("datetime")
                .HasColumnName("data_atualizacao_cotacao");
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
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Status)
                .HasColumnType("bit(1)")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .HasMaxLength(26)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .HasColumnName("email");
            entity.Property(e => e.Senha)
                .HasMaxLength(80)
                .HasColumnName("senha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
