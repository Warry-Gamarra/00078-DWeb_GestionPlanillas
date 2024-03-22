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
    public class TrabajadorCategoriaPlanillaController : Controller
    {
        private ITrabajadorServiceFacade _trabajadorServiceFacade;
        private ITrabajadorCategoriaPlanillaServiceFacade _trabajadorCategoriaPlanillaService;
        private IDependenciaServiceFacade _dependenciaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IGrupoTrabajoServiceFacade _grupoTrabajoServiceFacade;

        
        public TrabajadorCategoriaPlanillaController()
        {
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _grupoTrabajoServiceFacade = new GrupoTrabajoServiceFacade();
        }

        [HttpGet]
        public ActionResult CategoriasPlanillaAsignadas(int id)
        {
            ViewBag.Title = "Categorias planillas asignados";

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            ViewBag.CategoriaPlanillaDesc = _trabajadorCategoriaPlanillaService.ListarCategoriaPlanillaPorTrabajador(id)
                .Where(x => x.esCategoriaPrincipal).First().categoriaPlanillaDesc.ToUpper();

            return PartialView("_CategoriasPlanillaAsignadas", trabajador);
        }

        [HttpGet]
        public JsonResult ObtenerListaCategoriasPlanillaAsignadas(int id)
        {
            var result = new AjaxResponse();

            result.data = _trabajadorCategoriaPlanillaService.ListarCategoriaPlanillaPorTrabajador(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AsignarCategoriaPlanilla(int id)
        {
            ViewBag.Title = "Asignar a Planilla";

            ViewBag.Action = "AsignarCategoriaPlanilla";

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            var categoriaPlanillaID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajador.vinculoID);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(categoriaPlanillaExcluidaID: categoriaPlanillaID);

            ViewBag.ListaGruposTrabajo = _grupoTrabajoServiceFacade.ObtenerComboGruposTrabajo();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: false);

            var model = new TrabajadorCategoriaPlanillaModel()
            {
                trabajadorID = trabajador.trabajadorID.Value
            };

            return PartialView("_AsignarCategoriaPlanilla", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarCategoriaPlanilla(TrabajadorCategoriaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorCategoriaPlanillaService.GrabarTrabajadorCategoriaPlanilla(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.TrabajadorID = model.trabajadorID;

            return PartialView("_MsgAsignarCategoriaPlanilla", response);
        }

        [HttpGet]
        public ActionResult EditarCategoriaPlanilla(int id)
        {
            ViewBag.Title = "Asignar a Planilla";

            ViewBag.Action = "EditarCategoriaPlanilla";

            var model = _trabajadorCategoriaPlanillaService.ObtenerTrabajadorCategoriaPlanilla(id);

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(model.trabajadorID);

            var categoriaPlanillaPrincipalID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajador.vinculoID);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(
                categoriaPlanillaExcluidaID: categoriaPlanillaPrincipalID, incluirDeshabilitados: true, selectedItem: model.categoriaPlanillaID);

            ViewBag.ListaGruposTrabajo = _grupoTrabajoServiceFacade.ObtenerComboGruposTrabajo(incluirDeshabilitados: true, selectedItem: model.grupoTrabajoID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: true, selectedItem: model.dependenciaID);

            return PartialView("_AsignarCategoriaPlanilla", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCategoriaPlanilla(TrabajadorCategoriaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorCategoriaPlanillaService.GrabarTrabajadorCategoriaPlanilla(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.TrabajadorID = model.trabajadorID;

            return PartialView("_MsgAsignarCategoriaPlanilla", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstadoCategoriaPlanilla(int rowID, bool estaHabilitado)
        {
            var result = _trabajadorCategoriaPlanillaService.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EliminarCategoriaPlanilla(int id)
        {
            var result = _trabajadorCategoriaPlanillaService.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TrabajadoresAsignadosCategoriasPlanilla()
        {
            ViewBag.Title = "Trabajadores Asignados";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoresAsignados(int? idCategoria)
        {
            var result = new AjaxResponse();

            if (idCategoria.HasValue)
            {
                result.data = _trabajadorCategoriaPlanillaService.ListarTrabajadoresAsignadosCategoria(idCategoria.Value);
            }
            else
            {
                result.data = new List<TrabajadorCategoriaPlanillaModel>();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}