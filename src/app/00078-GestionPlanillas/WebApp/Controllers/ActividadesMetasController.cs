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
    public class ActividadesMetasController : Controller
    {
        private IDepActividadMetaServiceFacade _depActividadMetaServiceFacade;

        public ActividadesMetasController()
        {
            _depActividadMetaServiceFacade = new DepActividadMetaServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Actividades y Metas";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaDepActividadMetas()
        {
            var result = new AjaxResponse();

            result.data = _depActividadMetaServiceFacade.ListarDepActividadMetas();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Registro";

            ViewBag.Action = "Registrar";

            var model = new DepActividadMetaModel();

            return PartialView("_MantenimientoActividadMeta", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(DepActividadMetaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _depActividadMetaServiceFacade.GrabarDepActividadMeta(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarActividadMeta", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Registro";

            ViewBag.Action = "Actualizar";

            var model = _depActividadMetaServiceFacade.ObtenerDepActividadMeta(id);

            return PartialView("_MantenimientoActividadMeta", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(DepActividadMetaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _depActividadMetaServiceFacade.GrabarDepActividadMeta(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarActividadMeta", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _depActividadMetaServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}