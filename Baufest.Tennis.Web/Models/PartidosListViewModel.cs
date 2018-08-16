using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baufest.Tennis.Web.Models
{
    public class PartidosListViewModel
    {
        public IEnumerable<PartidoListViewModel> ListaDePartidos { get; set; }
    }
}