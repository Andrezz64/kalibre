using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace kalibre_api;

public partial class KalibreContext : DbContext
{
    public KalibreContext()
    {
    }

    public KalibreContext(DbContextOptions<KalibreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Despesa> Despesas { get; set; }

    public virtual DbSet<Receita> Receitas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // PM:1 - Mover a connections string para fora do código, seguir o link da documentação
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=kalibre;username=postgres;Password=1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(e => e.DespesaId).HasName("despesas_pkey");

            entity.ToTable("despesas");

            entity.Property(e => e.DespesaId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("despesaId");
            entity.Property(e => e.Data)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data");
            entity.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.Receitaid).HasName("receitas_pkey");

            entity.ToTable("receitas");

            entity.Property(e => e.Receitaid)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("receitaid");
            entity.Property(e => e.Data)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data");
            entity.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
