using System.Web.Mvc;
using System.Linq;
using Baufest.Tennis.Business;
using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Web.Models;

namespace Baufest.Tennis.Web.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly IJugadorBusiness jugadorBusiness;

        public JugadoresController()
        {
            this.jugadorBusiness = new JugadorBusiness();
        }

        public JugadoresController(IJugadorBusiness jugadorBusiness)
        {
            this.jugadorBusiness = jugadorBusiness;
        }

        public ActionResult Index()
        {
            var jugadores = jugadorBusiness.ListJugadores();
            var viewModel = new JugadoresListViewModel();
            viewModel.ListaDeJugadores = jugadores
                .Select(j => new JugadorViewModel(j))
                .ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var viewModel = new JugadorViewModel();
            return View(viewModel);
        }

        public ActionResult Editar(int id)
        {
            var viewModel = new JugadorViewModel(jugadorBusiness.GetJugador(id));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Agregar(JugadorViewModel jugadorViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var jugador = jugadorViewModel.ToEntity();
                jugadorBusiness.AddJugador(jugador);
                return RedirectToAction("Index");
            }

            return View(jugadorViewModel);
        }

        [HttpPost]
        public ActionResult Editar(JugadorViewModel jugadorViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var jugador = jugadorViewModel.ToEntity();
                jugadorBusiness.UpdateJugador(jugador);
                return RedirectToAction("Index");
            }

            return View(jugadorViewModel);
        }

        public ActionResult ReCalculateRanking(int id)
        {
            this.jugadorBusiness.ReCalculateRanking(id);

            return RedirectToAction("Index");
        }
    }
}