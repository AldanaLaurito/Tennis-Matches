using Baufest.Tennis.Business;
using Baufest.Tennis.Domain.Entities;
using Baufest.Tennis.Domain.Interfaces;
using Baufest.Tennis.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Baufest.Tennis.Web.Controllers
{
    public class CanchasController : Controller
    {
        private readonly ICanchaBusiness canchaBusiness;

        public CanchasController()
        {
            this.canchaBusiness = new CanchaBusiness();
        }

        public CanchasController(ICanchaBusiness canchaBusiness)
        {
            this.canchaBusiness = canchaBusiness;
        }

        public ActionResult Index()
        {
            var canchas = canchaBusiness.ListCanchas();

            var viewModel = new CanchasListViewModel
            {
                List = canchas.Select(cancha => new CanchaViewModel(cancha)
                {
                    PartidosEnElDia = this.canchaBusiness.CantPartidosEnFecha(cancha, DateTime.Now)
                })
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var cancha = new CanchaViewModel();
            return View(cancha);
        }

        public ActionResult Editar(int id)
        {
            var cancha = new CanchaViewModel(canchaBusiness.GetCancha(id));
            return View(cancha);
        }

        [HttpPost]
        public ActionResult Agregar(CanchaViewModel cancha)
        {
            if (this.ModelState.IsValid)
            {
                canchaBusiness.AddCancha(cancha.ToEntity());
                return RedirectToAction("Index");
            }

            return View(cancha);
        }

        [HttpPost]
        public ActionResult Editar(CanchaViewModel cancha)
        {
            if (this.ModelState.IsValid)
            {
                canchaBusiness.UpdateCancha(cancha.ToEntity());
                return RedirectToAction("Index");
            }

            return View(cancha);
        }
    }
}