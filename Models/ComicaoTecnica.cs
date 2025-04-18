using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Futebol.Models
{
    public class ComicaoTecnica
    {
        public int ID { get; set; }
        public string NomeDaComicao { get; set; }
        public Cargo Cargo { get; set; } // Enum para cargo
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataDeNascimento { get; set; }
        public int TimeID { get; set; }
        public Time Time { get; set; }
    }

    public enum Cargo
    {
        Treinador,
        Auxiliar,
        PreparadorFisico,
        Fisiologista,
        TreinadorDeGoleiros,
        Fisioterapeuta
    }
}