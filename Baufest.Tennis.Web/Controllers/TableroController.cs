using Baufest.Tennis.Business;
using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Web.Models;
using System.Web.Mvc;

namespace Baufest.Tennis.Web.Controllers
{
    public class TableroController : Controller
    {
        private readonly ITableroBusiness tableroBusiness;

        public TableroController()
        {
            this.tableroBusiness = new TableroBusiness();
        }

        public TableroController(ITableroBusiness tableroBusiness)
        {
            this.tableroBusiness = tableroBusiness;
        }

        public ActionResult Jugar(int id)
        {
            var viewModel = new TableroViewModel(tableroBusiness.IniciarPartido(id));
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SumarPuntoLocal(int id)
        {
            Partido partido = this.tableroBusiness.SumarPuntoLocal(id);
            var viewModel = new TableroViewModel(partido);

            return this.Json(new { success = true, partido = viewModel });
        }

        [HttpPost]
        public JsonResult SumarPuntoVisitante(int id)
        {
            Partido partido = this.tableroBusiness.SumarPuntoVisitante(id);
            var viewModel = new TableroViewModel(partido);

            return this.Json(new { success = true, partido = viewModel });
        }
    }
}