using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baufest.Tennis.Web.Models
{
    public class JugadoresListViewModel
    {
        public IEnumerable<JugadorViewModel> ListaDeJugadores { get; set; }
    }
}