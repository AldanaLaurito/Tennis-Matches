using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Baufest.Tennis.Business;
using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Web.Models;

namespace Baufest.Tennis.Web.Controllers
{
    public class PartidosController : Controller
    {
        private readonly IPartidoBusiness partidoBusiness;
        private readonly IJugadorBusiness jugadorBusiness;
        private readonly ICanchaBusiness canchaBusiness;

        public PartidosController()
        {
            this.partidoBusiness = new PartidoBusiness();
            this.jugadorBusiness = new JugadorBusiness();
            this.canchaBusiness = new CanchaBusiness();
        }

        public PartidosController(IPartidoBusiness partidoBusiness, IJugadorBusiness jugadorBusiness, ICanchaBusiness canchaBusiness)
        {
            this.partidoBusiness = partidoBusiness;
            this.jugadorBusiness = jugadorBusiness;
            this.canchaBusiness = canchaBusiness;
        }

        public ActionResult Index()
        {
            var viewModel = new PartidosListViewModel();
            viewModel.ListaDePartidos =  partidoBusiness
                .ListPartidos()
                .Select(p => new PartidoListViewModel(p))
                .ToList();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var viewModel = new PartidoCreateUpdateViewModel
                {
                    FechaComienzo = DateTime.Now.AddDays(1),
                    ListaDeJugadores = FillListaDeJugadores(),
					ListaDeCanchas = FillListaDeCanchas()
                };
            return View(viewModel);
        }

        private IEnumerable<SelectListItem> FillListaDeJugadores()
        {
            var jugadores = jugadorBusiness.ListJugadores();
            return jugadores.Select(
                x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString(),
                    Selected = false
                });
        }

		private IEnumerable<SelectListItem> FillListaDeCanchas()
        {
            var canchas = canchaBusiness.ListCanchas();
            
            return canchas.Select(
				x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString(),
                    Selected = false
                });
        }        
        public ActionResult Editar(int id)
        {
            var viewModel = new PartidoCreateUpdateViewModel(partidoBusiness.GetPartido(id));
            viewModel.ListaDeJugadores = FillListaDeJugadores();
			viewModel.ListaDeCanchas = FillListaDeCanchas();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Agregar(PartidoCreateUpdateViewModel partidoCreateUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var errores = new List<string>();
                var exito = partidoBusiness.AddPartido(partidoCreateUpdateViewModel.FechaComienzo, partidoCreateUpdateViewModel.IdJugadorLocal, partidoCreateUpdateViewModel.IdJugadorVisitante, partidoCreateUpdateViewModel.IdCancha, errores);
                if (exito)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in errores)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            partidoCreateUpdateViewModel.ListaDeJugadores = FillListaDeJugadores();
			partidoCreateUpdateViewModel.ListaDeCanchas = FillListaDeCanchas();
			
            return View(partidoCreateUpdateViewModel);
        }

        [HttpPost]
        public ActionResult Editar(PartidoCreateUpdateViewModel partidoCreateUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var errores = new List<string>();
                var exito = partidoBusiness.UpdatePartido(partidoCreateUpdateViewModel.Id, partidoCreateUpdateViewModel.FechaComienzo, partidoCreateUpdateViewModel.IdJugadorLocal, partidoCreateUpdateViewModel.IdJugadorVisitante, partidoCreateUpdateViewModel.IdCancha, errores);
                if (exito)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in errores)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            partidoCreateUpdateViewModel.ListaDeJugadores = FillListaDeJugadores();
			partidoCreateUpdateViewModel.ListaDeCanchas = FillListaDeCanchas();
			
            return View(partidoCreateUpdateViewModel);
        }

        public ActionResult Eliminar(int id)
        {
            var viewModel = new PartidoDeleteViewModel (partidoBusiness.GetPartido(id));
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Eliminar")]
        public ActionResult EliminarPost(int id)
        {
                var errores = new List<string>();
                var exito = partidoBusiness.DeletePartido(id, errores);
                if (exito)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in errores)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            
            return Eliminar(id);
        }

    }
}