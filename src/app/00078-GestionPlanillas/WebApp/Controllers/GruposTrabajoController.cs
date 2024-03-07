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
    public class GruposTrabajoController : Controller
    {
        private IGrupoTrabajoServiceFacade _grupoTrabajoServiceFacade;

        public GruposTrabajoController()
        {
            _grupoTrabajoServiceFacade = new GrupoTrabajoServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Grupos de trabajo";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaGruposTrabajo()
        {
            var result = new AjaxResponse();

            result.data = _grupoTrabajoServiceFacade.ListarGruposTrabajo();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Grupo de Trabajo";

            ViewBag.Action = "Registrar";

            var model = new GrupoTrabajoModel();

            return PartialView("_MantenimientoGrupoTrabajo", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(GrupoTrabajoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _grupoTrabajoServiceFacade.GrabarGrupoTrabajo(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarGrupoTrabajo", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Grupo de Trabajo";

            ViewBag.Action = "Actualizar";

            var model = _grupoTrabajoServiceFacade.ObtenerGrupoTrabajo(id);

            return PartialView("_MantenimientoGrupoTrabajo", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(GrupoTrabajoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _grupoTrabajoServiceFacade.GrabarGrupoTrabajo(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarGrupoTrabajo", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _grupoTrabajoServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _grupoTrabajoServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}