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
    public class ConceptosController : Controller
    {
        private IConceptoServiceFacade _conceptoServiceFacade;
        private ITipoConceptoServiceFacade _tipoConceptoServiceFacade;
        private IPlantillaPlanillaServiceFacade _plantillaPlanillaServiceFacade;

        public ConceptosController()
        {
            _conceptoServiceFacade = new ConceptoServiceFacade();
            _tipoConceptoServiceFacade = new TipoConceptoServiceFacade();
            _plantillaPlanillaServiceFacade = new PlantillaPlanillaServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Configuración de conceptos";

            var lista = _conceptoServiceFacade.ListarConceptos();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Concepto";

            ViewBag.Action = "Registrar";

            ViewBag.ListaTiposConceptos = _tipoConceptoServiceFacade.ListarTiposConceptos();

            var concepto = new ConceptoModel();

            return PartialView("_MantenimientoConcepto", concepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(ConceptoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _conceptoServiceFacade.GrabarConcepto(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarConcepto", response);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Concepto";

            ViewBag.Action = "Actualizar";

            ViewBag.ListaTiposConceptos = _tipoConceptoServiceFacade.ListarTiposConceptos();

            var concepto = _conceptoServiceFacade.ObtenerConcepto(id);

            return PartialView("_MantenimientoConcepto", concepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ConceptoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _conceptoServiceFacade.GrabarConcepto(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarConcepto", response);
        }

        [HttpPost]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _conceptoServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId, Url.Action("CambiarEstado", "Conceptos"));
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}