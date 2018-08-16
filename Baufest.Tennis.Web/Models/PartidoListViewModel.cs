using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Web.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Baufest.Tennis.Web.Models
{
    public class PartidoListViewModel
    {
        public int IdPartido { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Comienzo")]
        public DateTime FechaComienzo { get; set; }

        [Display(Name = "Jugador Local")]
        public JugadorViewModel JugadorLocal { get; set; }

        [Display(Name = "Jugador Visitante")]
        public JugadorViewModel JugadorVisitante { get; set; }
		
		[Display(Name = "Cancha")]
        public CanchaViewModel Cancha { get; set; }

        [Display(Name = "Estado")]
        public EstadoPartido Estado { get; set; }
        public string EstadoDisplay()
        {
            return  Estado.GetAttribute<DisplayAttribute>().Name;
        }


        public PartidoListViewModel()
        {

        }

        public PartidoListViewModel(Partido partidoEntity)
        {
            IdPartido = partidoEntity.Id;
            FechaComienzo = partidoEntity.FechaComienzo;
            JugadorLocal = new JugadorViewModel(partidoEntity.JugadorLocal);
            JugadorVisitante = new JugadorViewModel(partidoEntity.JugadorVisitante);
            Estado = partidoEntity.Estado;
            Cancha = new CanchaViewModel(partidoEntity.Cancha);
        }
    }
}