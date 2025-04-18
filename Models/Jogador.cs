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
}