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
        public DateTime DataDeNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public Posicao Posicao { get; set; } // Enum para posição
        public int CamisaNumero { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }
        public PeDominante PeDominante { get; set; } // Enum para peDominante
        public int TimeID { get; set; }
        public Time Time { get; set; }
    }

    public enum Posicao
    {
        Goleiro,
        Zagueiro,
        Volante,
        Meia,
        Atacante,
        Lateral,
        Ala,
        Ponta,
        Técnico
    }

    public enum PeDominante
    {
        Direito,
        Esquerdo,
        Ambidestro
    }
}