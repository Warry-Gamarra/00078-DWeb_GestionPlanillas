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
    public class DepActividadesMetasController : Controller
    {
        private IDepActividadMetaServiceFacade _depActividadMetaServiceFacade;
        private IActividadServiceFacade _actividadServiceFacade;
        private IMetaServiceFacade _metaServiceFacade;
        private IDependenciaServiceFacade _dependenciaServiceFacade;

        public DepActividadesMetasController()
        {
            _depActividadMetaServiceFacade = new DepActividadMetaServiceFacade();
            _actividadServiceFacade = new ActividadServiceFacade();
            _metaServiceFacade = new MetaServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
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

            ViewBag.ListaActividades = _actividadServiceFacade.ObtenerComboActividades();

            ViewBag.ListaMetas = _metaServiceFacade.ObtenerComboMetas();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias();

            return PartialView("_MantenimientoDepActividadMeta", model);
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

            return PartialView("_MsgRegistrarDepActividadMeta", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Editar Registro";

            ViewBag.Action = "Actualizar";

            var model = _depActividadMetaServiceFacade.ObtenerDepActividadMeta(id);

            ViewBag.ListaActividades = _actividadServiceFacade.ObtenerComboActividades(model.actividadID);

            ViewBag.ListaMetas = _metaServiceFacade.ObtenerComboMetas(model.metaID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: true, selectedItem: model.dependenciaID);

            return PartialView("_MantenimientoDepActividadMeta", model);
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

            return PartialView("_MsgRegistrarDepActividadMeta", response);
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