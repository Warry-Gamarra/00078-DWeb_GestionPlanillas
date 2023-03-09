using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    }
}