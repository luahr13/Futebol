using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class Time
    {
        public int ID { get; set; }
        public string NomeDoTime { get; set; }

        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();
        public List<ComicaoTecnica> ComicaoTecnica { get; set; } = new List<ComicaoTecnica>();

        public class FutebolDBContext : DbContext
        {
            public DbSet<Time> Times { get; set; }
        }
    }
}