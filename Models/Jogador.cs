using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Futebol.Models
{
    public class Jogador
    {
        public int ID { get; set; }
        public string NomeDoJogador { get; set; }
        public int CamisaNumero { get; set; }

        public int TimeID { get; set; }
        public Time Time { get; set; }
    }
    public class FutebolDBContext : DbContext
    {
        public DbSet<Jogador> Jogadores { get; set; }

        public System.Data.Entity.DbSet<Futebol.Models.Time> Times { get; set; }

        public System.Data.Entity.DbSet<Futebol.Models.ComicaoTecnica> ComicaoTecnicas { get; set; }
    }
}