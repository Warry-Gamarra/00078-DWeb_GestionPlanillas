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

        private ITrabajadorServiceFacade _trabajadorServiceFacade;

        private IAdministrativoServiceFacade _administrativoServiceFacade;

        private IDocenteServiceFacade _docenteServiceFacade;

        public TrabajadoresController()
        {
            _trabajadorServiceFacade = new TrabajadorServiceFacade();

            _administrativoServiceFacade = new AdministrativoServiceFacade();

            _docenteServiceFacade = new DocenteServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Consulta";

            var lista = _trabajadorServiceFacade.ListarTrabajadores();

            return View(lista);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Trabajador";

            var trabajador = _trabajadorServiceFacade.ListarTrabajadores().Where(x => x.I_TrabajadorID == id).FirstOrDefault();

            //if (trabajador.I_VinculoID == 1)//administrativo
            //{
            //    var administrativo = _administrativoServiceFacade.ListarAdministrativoPorTrabajadorID(trabajador.I_TrabajadorID).FirstOrDefault();

            //    return PartialView("_MantenimientoAdministrativo", administrativo);
            //}

            //if (trabajador.I_VinculoID == 4)//docente
            //{
            //    var docente = _docenteServiceFacade.ListarDocentePorTrabajadorID(trabajador.I_TrabajadorID).FirstOrDefault();

            //    return PartialView("_MantenimientoDocente", docente);
            //}

            return PartialView("_DetalleTrabajador", trabajador);
        }
    }
}