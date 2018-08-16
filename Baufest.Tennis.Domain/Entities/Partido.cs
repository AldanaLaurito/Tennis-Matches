using System;
using System.ComponentModel.DataAnnotations;

using Baufest.Tennis.Domain.Enums;

namespace Baufest.Tennis.Domain.Entities
{
    public class Partido
    {
        [Key]
        public int Id { get; set; }

        public DateTime FechaComienzo { get; set; }

        public EstadoPartido Estado { get; set; }

        public virtual Jugador JugadorLocal { get; set; }

        public int JugadorLocalId { get; set; }

        public virtual Jugador JugadorVisitante { get; set; }

        public int JugadorVisitanteId { get; set; }

        [StringLength(3, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string PuntosGameLocal { get; set; }

        public int ScoreLocal { get; set; }

        public int ScoreVisitante { get; set; }

        public int GamesLocal { get; set; }
       
        [StringLength(3, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        public string PuntosGameVisitante { get; set; }

        public int GamesVisitante { get; set; }


        public virtual Cancha Cancha { get; set; }

        public int CanchaId { get; set; }
    }
}
