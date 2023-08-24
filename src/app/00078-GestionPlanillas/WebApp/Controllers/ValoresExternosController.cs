using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    [Authorize]
    public class ValoresExternosController : Controller
    {
        IValorExternoConceptoServiceFacade _valorExternoConceptoServiceFacade;
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;

        public ValoresExternosController()
        {
            _valorExternoConceptoServiceFacade = new ValorExternoConceptoServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Cargar información externa";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ObtenerInformacionArchivo(HttpPostedFileBase inputFile)
        {
            var result = new AjaxResponse();

            try
            {
                if (inputFile == null)
                {
                    throw new NullReferenceException("Debe seleccionar un archivo.");
                }

                result.data = _valorExternoConceptoServiceFacade.ObtenerListaValoresDeConceptos(inputFile);

                result.success = true;
            }
            catch (Exception ex)
            {
                result.message = ex.Message;
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GuardarInformacion(string fileName)
        {
            var result = _valorExternoConceptoServiceFacade.GrabarValoresExternos(fileName, WebSecurity.CurrentUserId);

            var response = new AjaxResponse()
            {
                success = result.Success,
                message = result.Message
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DescargarResultadoLectura(string fileName)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _valorExternoConceptoServiceFacade.ObtenerResultadoLectura(FormatoArchivo.XLSX, fileName);

                return File(fileContent.fileContent, fileContent.contentType, fileContent.fileName);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        [HttpGet]
        public ActionResult Mantenimiento()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños();

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Mantenimiento de Información Externa";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAnio(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaInformaciónExterna(int? anio, int? mes, int? idCategoria)
        {
            var result = new AjaxResponse();

            List<ValorExternoConceptoModel> lista;

            if (anio.HasValue && mes.HasValue && idCategoria.HasValue)
            {
                lista = _valorExternoConceptoServiceFacade.ListarValoresExternos(anio.Value, mes.Value, idCategoria.Value)
                    .ToList();
            }
            else
            {
                lista = new List<ValorExternoConceptoModel>();
            }

            result.data = lista;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Concepto";

            ViewBag.Action = "Actualizar";

            var valorExterno = _valorExternoConceptoServiceFacade.ObtenerValorExterno(id);

            return PartialView("_MantenimientoValorExterno", valorExterno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ValorExternoConceptoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _valorExternoConceptoServiceFacade.ActualizarValorExternoConcepto(
                    model.conceptoExternoValorID, model.valorConcepto, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgActualizarValorExternoConcepto", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _valorExternoConceptoServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}