using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class Partidas
    {
        public int ID { get; set; }
        public int TimeCasaID { get; set; }
        public Time TimeCasa { get; set; }
        public string Estadio { get; set; } // Estádio da partida

        public int TimeVisitanteID { get; set; }
        public Time TimeVisitante { get; set; }
    }
}