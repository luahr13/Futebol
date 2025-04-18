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
        public DbSet<ComicaoTecnica> ComicaoTecnica { get; set; }
        public DbSet<Partidas> Partidas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuração para o relacionamento TimeCasa
            modelBuilder.Entity<Partidas>()
                .HasRequired(p => p.TimeCasa)
                .WithMany()
                .HasForeignKey(p => p.TimeCasaID)
                .WillCascadeOnDelete(false); // Sem exclusão em cascata

            // Configuração para o relacionamento TimeVisitante
            modelBuilder.Entity<Partidas>()
                .HasRequired(p => p.TimeVisitante)
                .WithMany()
                .HasForeignKey(p => p.TimeVisitanteID)
                .WillCascadeOnDelete(false); // Sem exclusão em cascata

            base.OnModelCreating(modelBuilder);
        }

    }
}