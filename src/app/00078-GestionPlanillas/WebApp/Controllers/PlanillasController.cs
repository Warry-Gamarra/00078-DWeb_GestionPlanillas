using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    public class PlanillasController : Controller
    {
        private IPlanillaServiceFacade _planillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;

        public PlanillasController()
        {
            _planillaServiceFacade = new PlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Resumen Planilla de Trabajadores";

            var lista = _planillaServiceFacade.ListarResumenPlanillaTrabajador();

            return View(lista);
        }

        public ActionResult Generar()
        {
            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generar(int anio, int mes, int? idCategoria)
        {
            ViewBag.Title = "Generar Planillas";

            return View();
        }
    }
}