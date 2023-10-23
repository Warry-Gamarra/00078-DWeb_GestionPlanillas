using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    public class DependenciasController : Controller
    {
        private IDependenciaServiceFacade _dependenciaServiceFacade;

        public DependenciasController()
        {
            _dependenciaServiceFacade = new DependenciaServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenimiento de Dependencias";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaDependencias()
        {
            var result = new AjaxResponse();

            result.data = _dependenciaServiceFacade.ListarDependencias();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nueva Dependencia";

            ViewBag.Action = "Registrar";

            var model = new DependenciaModel();

            return PartialView("_MantenimientoDependencia", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(DependenciaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _dependenciaServiceFacade.GrabarDependencia(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarDependencia", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Dependencia";

            ViewBag.Action = "Actualizar";

            var model = _dependenciaServiceFacade.ObtenerDependencia(id);

            return PartialView("_MantenimientoDependencia", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(DependenciaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _dependenciaServiceFacade.GrabarDependencia(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarDependencia", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _dependenciaServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _dependenciaServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}