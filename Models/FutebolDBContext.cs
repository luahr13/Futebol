using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class FutebolDBContext : DbContext
    {
        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<ComissaoTecnica> ComissaoTecnica { get; set; }
        public DbSet<Partidas> Partidas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuração para o relacionamento TimeCasa
            modelBuilder.Entity<Partidas>()
                .HasRequired(p => p.TimeCasa)
                .WithMany()
                .HasForeignKey(p => p.TimeCasaID)
                .WillCascadeOnDelete(false);

            // Configuração para o relacionamento TimeVisitante
            modelBuilder.Entity<Partidas>()
                .HasRequired(p => p.TimeVisitante)
                .WithMany()
                .HasForeignKey(p => p.TimeVisitanteID)
                .WillCascadeOnDelete(false);

            // Configuração para o relacionamento TimeID em EstatisticasPartida
            modelBuilder.Entity<EstatisticasPartida>()
                .HasRequired(e => e.Time)
                .WithMany()
                .HasForeignKey(e => e.TimeID)
                .WillCascadeOnDelete(false);

            // Configuração para o relacionamento PartidaID em EstatisticasPartida
            modelBuilder.Entity<EstatisticasPartida>()
                .HasRequired(e => e.Partida)
                .WithMany(p => p.Estatisticas)
                .HasForeignKey(e => e.PartidaID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstatisticasPartida>()
                .HasRequired(e => e.Time)
                .WithMany()
                .HasForeignKey(e => e.TimeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstatisticasPartida>()
                .HasRequired(e => e.Partida)
                .WithMany(p => p.Estatisticas)
                .HasForeignKey(e => e.PartidaID)
                .WillCascadeOnDelete(false);

            // Chamada ao método base deve vir APÓS todas as configurações
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Futebol.Models.EstatisticasPartida> EstatisticasPartidas { get; set; }
    }
}