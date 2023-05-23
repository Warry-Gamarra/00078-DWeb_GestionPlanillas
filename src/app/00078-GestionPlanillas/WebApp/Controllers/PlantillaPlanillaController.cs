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
    public class PlantillaPlanillaController : Controller
    {
        private IPlantillaPlanillaServiceFacade _plantillaPlanillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IPlantillaPlanillaConceptoServiceFacade _plantillaPlanillaConceptoServiceFacade;

        public PlantillaPlanillaController()
        {
            _plantillaPlanillaServiceFacade = new PlantillaPlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _plantillaPlanillaConceptoServiceFacade = new PlantillaPlanillaConceptoServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Configuración de Plantillas";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaPlantillasPlanilla()
        {
            var result = new AjaxResponse();

            result.data = _plantillaPlanillaServiceFacade.ListarPlantillasPlanilla();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nueva Plantilla";

            ViewBag.Action = "Registrar";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

            var concepto = new PlantillaPlanillaModel();

            return PartialView("_MantenimientoPlantillaPlanilla", concepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PlantillaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _plantillaPlanillaServiceFacade.GrabarPlantillaPlanilla(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarPlantillaPlanilla", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle de la Plantilla";

            ViewBag.Action = "Actualizar";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

            var concepto = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanilla(id);

            return PartialView("_MantenimientoPlantillaPlanilla", concepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(PlantillaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _plantillaPlanillaServiceFacade.GrabarPlantillaPlanilla(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarPlantillaPlanilla", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _plantillaPlanillaServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AsignarConceptos(int id)
        {
            ViewBag.Title = "Conceptos asignados";

            ViewBag.Action = "Actualizar";

            var model = _plantillaPlanillaConceptoServiceFacade.ListarConceptosAsignados(id);

            return PartialView("_ConceptosAsignados");
        }
    }
}