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
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private ICategoriaPresupuestalServiceFacade _categoriaPresupuestalServiceFacade;

        public DepActividadesMetasController()
        {
            _depActividadMetaServiceFacade = new DepActividadMetaServiceFacade();
            _actividadServiceFacade = new ActividadServiceFacade();
            _metaServiceFacade = new MetaServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _categoriaPresupuestalServiceFacade = new CategoriaPresupuestalServiceFacade();
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

            ViewBag.ListaAños = _periodoServiceFacade.ObtenerComboAños();

            ViewBag.ListaActividades = _actividadServiceFacade.ObtenerComboActividades();

            ViewBag.ListaMetas = _metaServiceFacade.ObtenerComboMetas();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias();

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            ViewBag.ListarCategoriaPresupuestal = _categoriaPresupuestalServiceFacade.ObtenerComboCategoriaPresupuestal();

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

            ViewBag.ListaAños = _periodoServiceFacade.ObtenerComboAños(selectedItem: model.anio);

            ViewBag.ListaActividades = _actividadServiceFacade.ObtenerComboActividades(model.actividadID);

            ViewBag.ListaMetas = _metaServiceFacade.ObtenerComboMetas(model.metaID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: true, selectedItem: model.dependenciaID);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(incluirDeshabilitados: true, selectedItem: model.dependenciaID);

            ViewBag.ListarCategoriaPresupuestal = _categoriaPresupuestalServiceFacade.ObtenerComboCategoriaPresupuestal(incluirDeshabilitados: true, selectedItem: model.categoriaPresupuestalID);

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