using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetoApi.Models;

namespace ProjetoApi.Models
{
    public partial class AvisoUrgenteContext : DbContext
    {

        public AvisoUrgenteContext(DbContextOptions<AvisoUrgenteContext> options)
            : base(options)
        {

        }

        public virtual DbSet<ProcessoMovimentacoes> ProcessoMovimentacoes { get; set; }
        public virtual DbSet<Processos> Processos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessoMovimentacoes>(entity =>
            {
                entity.HasKey(e => e.ProcessoMovimentacaoId);

            });

            modelBuilder.Entity<Processos>(entity =>
            {
                entity.HasKey(e => e.NumeroProcesso);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
