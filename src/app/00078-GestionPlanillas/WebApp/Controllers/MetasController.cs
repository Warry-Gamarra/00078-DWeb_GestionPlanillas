using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade.Implementations;
using WebApp.ServiceFacade;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    public class MetasController : Controller
    {
        private IMetaServiceFacade _metaServiceFacade;

        public MetasController()
        {
            _metaServiceFacade = new MetaServiceFacade();
        }

        [HttpGet]
        public JsonResult ObtenerListaMetas()
        {
            var result = new AjaxResponse();

            result.data = _metaServiceFacade.ListarMetas();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nueva Meta";

            ViewBag.Action = "Registrar";

            var meta = new MetaModel();

            return PartialView("_MantenimientoMeta", meta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(MetaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _metaServiceFacade.GrabarMeta(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarMeta", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle de la Meta";

            ViewBag.Action = "Actualizar";

            var meta = _metaServiceFacade.ObtenerMeta(id);

            return PartialView("_MantenimientoMeta", meta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(MetaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _metaServiceFacade.GrabarMeta(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarMeta", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _metaServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}