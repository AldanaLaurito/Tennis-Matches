using Baufest.Tennis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Baufest.Tennis.Web.Models
{
    public class PartidoCreateUpdateViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Comienzo")]
        public DateTime FechaComienzo { get; set; }

        [Display(Name = "Jugador Local")]
        public int IdJugadorLocal { get; set; }

        [Display(Name = "Jugador Visitante")]
        public int IdJugadorVisitante { get; set; }
		
		[Display(Name = "Cancha")]
        public int IdCancha { get; set; }

        public IEnumerable<SelectListItem> ListaDeJugadores { get; set; }
		
        public IEnumerable<SelectListItem> ListaDeCanchas { get; set; }

        public PartidoCreateUpdateViewModel()
        {

        }
        public PartidoCreateUpdateViewModel(Partido partidoEntity)
        {
            Id = partidoEntity.Id;
            FechaComienzo = partidoEntity.FechaComienzo;
            IdJugadorLocal = partidoEntity.JugadorLocal.Id;
            IdJugadorVisitante = partidoEntity.JugadorVisitante.Id;
        }
    }
}