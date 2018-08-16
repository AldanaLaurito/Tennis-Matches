using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Web.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Web.Models
{
    public class PartidoDeleteViewModel
    {
        public int Id { get; set; }
        public DateTime FechaComienzo { get; set; }

        [Display(Name = "Jugador Local")]
        public string JugadorLocalNombre { get; set; }

        [Display(Name = "Jugador Visitante")]
        public string JugadorVisitanteNombre { get; set; }

        

        public PartidoDeleteViewModel()
        {

        }

        public PartidoDeleteViewModel(Partido partidoEntity)
        {
            Id = partidoEntity.Id;
            FechaComienzo = partidoEntity.FechaComienzo;
            JugadorLocalNombre = partidoEntity.JugadorLocal.Nombre;
            JugadorVisitanteNombre = partidoEntity.JugadorVisitante.Nombre;
        }
    }
}