using System.Collections.Generic;

namespace Futebol.Models
{
    public class Time
    {
        public int ID { get; set; }
        public string NomeDoTime { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public int AnoFundacao { get; set; }
        public string Estadio { get; set; }
        public int CapacidadeEstadio { get; set; }
        public string CoresUniforme { get; set; }


        public List<Jogador> Jogadores { get; set; } = new List<Jogador>();
        public List<ComicaoTecnica> ComicaoTecnica { get; set; } = new List<ComicaoTecnica>();
    }
}