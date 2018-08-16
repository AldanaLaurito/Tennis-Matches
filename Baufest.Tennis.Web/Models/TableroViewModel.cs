using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Enums;
using Baufest.Tennis.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Baufest.Tennis.Web.Models
{
    public class TableroViewModel
    {
        public int Id { get; set; }

        public string JugadorLocalNombre { get; set; }
        public string JugadorVisitanteNombre { get; set; }

        public string PuntosGameLocal { get; set; }
        public string PuntosGameVisitante { get; set; }

        public int GamesLocal { get; set; }
        public int GamesVisitante { get; set; }

        public EstadoPartido Estado { get; set; }

        public string EstadoDisplay()
        {
            return Estado.GetAttribute<DisplayAttribute>().Name;
        }

        public TableroViewModel()
        {
                
        }

        public TableroViewModel(Partido partidoEntity)
        {
            Id = partidoEntity.Id;
            JugadorLocalNombre = partidoEntity.JugadorLocal.Nombre;
            JugadorVisitanteNombre = partidoEntity.JugadorVisitante.Nombre;
            PuntosGameLocal = partidoEntity.PuntosGameLocal;
            PuntosGameVisitante = partidoEntity.PuntosGameVisitante;
            GamesLocal = partidoEntity.GamesLocal;
            GamesVisitante = partidoEntity.GamesVisitante;
            Estado = partidoEntity.Estado;
        }
    }
}