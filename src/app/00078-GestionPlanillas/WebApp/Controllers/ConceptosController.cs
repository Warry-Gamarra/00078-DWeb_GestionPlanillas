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

        public ConceptosController()
        {
            _conceptoServiceFacade = new ConceptoServiceFacade();
            _tipoConceptoServiceFacade = new TipoConceptoServiceFacade();
        }

        [HttpGet]
        public JsonResult ObtenerListaConceptos()
        {
            var result = new AjaxResponse();

            result.data = _conceptoServiceFacade.ListarConceptos();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Concepto";

            ViewBag.Action = "Registrar";

            ViewBag.ListaTiposConceptos = _tipoConceptoServiceFacade.ObtenerComboTiposConceptos();

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

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Concepto";

            ViewBag.Action = "Actualizar";

            var concepto = _conceptoServiceFacade.ObtenerConcepto(id);

            ViewBag.ListaTiposConceptos = _tipoConceptoServiceFacade.ObtenerComboTiposConceptos(selectedItem: concepto.conceptoID);

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
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _conceptoServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}