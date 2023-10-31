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
    public class ActividadesController : Controller
    {
        private IActividadServiceFacade _actividadServiceFacade;

        public ActividadesController()
        {
            _actividadServiceFacade = new ActividadServiceFacade();
        }

        [HttpGet]
        public JsonResult ObtenerListaActividades()
        {
            var result = new AjaxResponse();

            result.data = _actividadServiceFacade.ListarActividades();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nueva Actividad";

            ViewBag.Action = "Registrar";

            var actividad = new ActividadModel();

            return PartialView("_MantenimientoActividad", actividad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(ActividadModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _actividadServiceFacade.GrabarActividad(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarActividad", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle de la Actividad";

            ViewBag.Action = "Actualizar";

            var actividad = _actividadServiceFacade.ObtenerActividad(id);

            return PartialView("_MantenimientoActividad", actividad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ActividadModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _actividadServiceFacade.GrabarActividad(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarActividad", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _actividadServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}