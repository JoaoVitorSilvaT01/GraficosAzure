using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AzureOrbis;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaseResumo> BaseResumos { get; set; }

    public virtual DbSet<ClientBase> ClientBases { get; set; }

    public virtual DbSet<ClientBase1> ClientBases1 { get; set; }

    public virtual DbSet<ClientsBalance> ClientsBalances { get; set; }

    public virtual DbSet<ClientsDistribution> ClientsDistributions { get; set; }

    public virtual DbSet<Consortium> Consortia { get; set; }

    public virtual DbSet<ForecastConsume> ForecastConsumes { get; set; }

    public virtual DbSet<ForecastGeneration> ForecastGenerations { get; set; }

    public virtual DbSet<ForecastGenerator> ForecastGenerators { get; set; }

    public virtual DbSet<GeneratorBase> GeneratorBases { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("pg_catalog", "azure")
            .HasPostgresExtension("pg_catalog", "pgaadauth")
            .HasPostgresExtension("pglogical", "pglogical");

        modelBuilder.Entity<BaseResumo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("base_resumos", "development");

            entity.Property(e => e.ArquivoXml).HasColumnName("arquivo_xml");
            entity.Property(e => e.DtBase).HasColumnName("dt_base");
            entity.Property(e => e.DtProxSaldoExpirar).HasColumnName("dt_prox_saldo_expirar");
            entity.Property(e => e.Empresa).HasColumnName("empresa");
            entity.Property(e => e.Instalacao).HasColumnName("instalacao");
            entity.Property(e => e.Modalidade).HasColumnName("modalidade");
            entity.Property(e => e.Periodo).HasColumnName("periodo");
            entity.Property(e => e.PeriodoUso).HasColumnName("periodo_uso");
            entity.Property(e => e.PostoHorario).HasColumnName("posto_horario");
            entity.Property(e => e.QtdCompensacao).HasColumnName("qtd_compensacao");
            entity.Property(e => e.QtdConsumo).HasColumnName("qtd_consumo");
            entity.Property(e => e.QtdGeracao).HasColumnName("qtd_geracao");
            entity.Property(e => e.QtdProxSaldoExpirar).HasColumnName("qtd_prox_saldo_expirar");
            entity.Property(e => e.QtdRecebimento).HasColumnName("qtd_recebimento");
            entity.Property(e => e.QtdSaldoAnt).HasColumnName("qtd_saldo_ant");
            entity.Property(e => e.QtdSaldoAtual).HasColumnName("qtd_saldo_atual");
            entity.Property(e => e.QtdSaldoExpirar).HasColumnName("qtd_saldo_expirar");
            entity.Property(e => e.QtdTransferencia).HasColumnName("qtd_transferencia");
            entity.Property(e => e.Quota).HasColumnName("quota");
            entity.Property(e => e.Usina).HasColumnName("usina");
        });

        modelBuilder.Entity<ClientBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("\"ClientBase\"");

            entity.Property(e => e.CemigId).HasColumnName("Cemig_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
        });

        modelBuilder.Entity<ClientBase1>(entity =>
        {
            entity.ToTable("ClientBase", "development");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Cep).HasMaxLength(8);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ClientNumber).HasMaxLength(50);
            entity.Property(e => e.CpfCnpj)
                .HasMaxLength(18)
                .HasColumnName("Cpf_Cnpj");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.Distributor).HasMaxLength(50);
            entity.Property(e => e.Installation).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status).HasDefaultValueSql("'ATIVO'::text");
            entity.Property(e => e.Uf).HasMaxLength(2);
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_At");
        });

        modelBuilder.Entity<ClientsBalance>(entity =>
        {
            entity.ToTable("ClientsBalance", "development");

            entity.HasIndex(e => e.ClientId, "IX_ClientsBalance_Client_Id");

            entity.Property(e => e.BalanceGdvalue).HasColumnName("BalanceGDValue");
            entity.Property(e => e.ClientId).HasColumnName("Client_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsBalances).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ClientsDistribution>(entity =>
        {
            entity.ToTable("ClientsDistribution", "development");

            entity.HasIndex(e => e.ClientId, "IX_ClientsDistribution_Client_Id");

            entity.HasIndex(e => e.GeneratorId, "IX_ClientsDistribution_Generator_Id");

            entity.Property(e => e.ClientId).HasColumnName("Client_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.GeneratorId).HasColumnName("Generator_Id");
            entity.Property(e => e.Status).HasDefaultValueSql("'CONSTRUCTION'::text");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsDistributions).HasForeignKey(d => d.ClientId);

            entity.HasOne(d => d.Generator).WithMany(p => p.ClientsDistributions).HasForeignKey(d => d.GeneratorId);
        });

        modelBuilder.Entity<Consortium>(entity =>
        {
            entity.ToTable("Consortium", "development");

            entity.HasIndex(e => e.UserId, "IX_Consortium_User_Id");

            entity.Property(e => e.ClientNumber).HasMaxLength(80);
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Consortia).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ForecastConsume>(entity =>
        {
            entity.ToTable("ForecastConsume", "development");

            entity.HasIndex(e => e.ClientId, "IX_ForecastConsume_Client_Id");

            entity.Property(e => e.ClientId).HasColumnName("Client_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");

            entity.HasOne(d => d.Client).WithMany(p => p.ForecastConsumes).HasForeignKey(d => d.ClientId);
        });

        modelBuilder.Entity<ForecastGeneration>(entity =>
        {
            entity.ToTable("ForecastGeneration", "development");

            entity.HasIndex(e => e.GeneratorId, "IX_ForecastGeneration_Generator_Id");

            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.GeneratorId).HasColumnName("Generator_Id");

            entity.HasOne(d => d.Generator).WithMany(p => p.ForecastGenerations).HasForeignKey(d => d.GeneratorId);
        });

        modelBuilder.Entity<ForecastGenerator>(entity =>
        {
            entity.HasNoKey().ToTable("forecast_generators", "development");  
            entity.Property(e => e.Usina).HasColumnName("Usina");
            entity.Property(e => e.MesAno).HasColumnName("Mês/Ano");
            entity.Property(e => e.Geracao).HasColumnName("Gerção");
        });


        modelBuilder.Entity<GeneratorBase>(entity =>
        {
            entity.ToTable("GeneratorBase", "development");

            entity.HasIndex(e => e.ConsortiumId, "IX_GeneratorBase_ConsortiumId");

            entity.Property(e => e.CemigId)
                .HasMaxLength(50)
                .HasColumnName("Cemig_Id");
            entity.Property(e => e.ClientNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.Generator).HasMaxLength(80);
            entity.Property(e => e.Installation).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Consortium).WithMany(p => p.GeneratorBases).HasForeignKey(d => d.ConsortiumId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "development");

            entity.Property(e => e.Role).HasDefaultValueSql("'NORMAL'::text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
