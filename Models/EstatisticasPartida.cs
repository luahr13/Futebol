using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class EstatisticasPartida
    {
        public int ID { get; set; } // Identificador único da estatística

        public int PartidaID { get; set; } // Relaciona a partida
        public Partidas Partida { get; set; }

        public int TimeID { get; set; } // Indica qual time fez o gol
        public Time Time { get; set; }

        public int JogadorID { get; set; } // Indica qual jogador fez o gol
        public Jogador Jogador { get; set; }

        public int SequenciaGol { get; set; } // Sequência do gol (Gol 1, Gol 2, etc.)
    }
}