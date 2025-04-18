using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class ComicaoTecnica
    {
        public int ID { get; set; }
        public string NomeDaComicao { get; set; }
        public int TimeID { get; set; }
        public Time Time { get; set; }
    }
}