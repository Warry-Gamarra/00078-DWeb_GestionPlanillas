using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    public class TrabajadoresController : Controller
    {

        private ITrabajadorServiceFacade trabajadorServiceFacade;

        public TrabajadoresController()
        {
            trabajadorServiceFacade = new TrabajadorServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Consulta";

            var lista = trabajadorServiceFacade.ListarTrabajadores();

            return View(lista);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Trabajador";

            var model = new TrabajadorModel();

            return PartialView("_MantenimientoDocente", model);
        }
    }
}