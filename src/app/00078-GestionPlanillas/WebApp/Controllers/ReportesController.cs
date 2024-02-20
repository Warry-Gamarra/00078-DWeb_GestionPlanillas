using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    public class ReportesController : Controller
    {
        private IPlanillaServiceFacade _planillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ITrabajadorServiceFacade _trabajadorServiceFacade;

        public ReportesController()
        {
            _planillaServiceFacade = new PlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
        }

        [HttpGet]
        public ActionResult ResumenPlanilla()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Resumen General";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerResumenPlanilla(int? anio, int? mes, int? idCategoria)
        {
            var result = new AjaxResponse();

            IEnumerable<ResumenPlanillaTrabajadorModel> lista;

            if (anio.HasValue && mes.HasValue && idCategoria.HasValue)
            {
                lista = _planillaServiceFacade.ListarResumenPlanillaTrabajador(anio.Value, mes.Value, idCategoria.Value);
            }
            else
            {
                lista = new List<ResumenPlanillaTrabajadorModel>();
            }

            result.data = lista;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DescargaResumenPlanilla(int anio, int mes, int idCategoria)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _planillaServiceFacade.ListarResumenPlanillaTrabajador(anio, mes, idCategoria, FormatoArchivo.XLSX);

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
        public ActionResult ResumenActividadYDependencia(int? anio, int? mes, int? idCategoria)
        {
            ReporteResumenPorActividadYDependencia model = null;

            bool aplicarBusqueda = (anio.HasValue && mes.HasValue && idCategoria.HasValue);

            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            anio = anio.HasValue ? anio.Value : listaAños.First().Value.AsInt();

            var listaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(anio.Value);

            mes = mes.HasValue ? mes.Value : listaMeses.First().Value.AsInt();

            var listaCatPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            idCategoria = idCategoria.HasValue ? idCategoria.Value : listaCatPlanillas.First().Value.AsInt();

            if (aplicarBusqueda)
            {
                model = _planillaServiceFacade.ObtenerReporteResumenPorActividadYDependencia(anio.Value, mes.Value, idCategoria.Value);
            }

            ViewBag.Title = "Resumen Planilla por Dependencia";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = listaMeses;

            ViewBag.ListaCategoriasPlanillas = listaCatPlanillas;

            return View(model);
        }

        [HttpGet]
        public ActionResult DescargaResumenActividadYDependencia(int anio, int mes, int idCategoria)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _planillaServiceFacade.ObtenerReporteResumenPorActividadYDependencia(anio, mes, idCategoria, FormatoArchivo.XLSX);

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
        public ActionResult ResumenSIAF(int? anio, int? mes)
        {
            ReporteResumenSIAF model = null;

            bool aplicarBusqueda = (anio.HasValue && mes.HasValue);

            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            anio = anio.HasValue ? anio.Value : listaAños.First().Value.AsInt();

            var listaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(anio.Value);

            mes = mes.HasValue ? mes.Value : listaMeses.First().Value.AsInt();

            if (aplicarBusqueda)
            {
                model = _planillaServiceFacade.ObtenerReporteResumenSIAF(anio.Value, mes.Value);
            }

            ViewBag.Title = "Resumen SIAF";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = listaMeses;

            return View(model);
        }

        [HttpGet]
        public ActionResult DescargaResumenSIAF(int anio, int mes)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _planillaServiceFacade.ObtenerReporteResumenSIAF(anio, mes, FormatoArchivo.XLSX);

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
        public ActionResult DetallesPlanillas()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Detalles de Planillas";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(año);

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoresConPlanilla(int? anio, int? mes)
        {
            var result = new AjaxResponse();

            IEnumerable<TrabajadorConPlanillaModel> lista;

            if (anio.HasValue && mes.HasValue)
            {
                lista = _trabajadorServiceFacade.ListarTrabajadoresConPlanilla(anio.Value, mes.Value);
            }
            else
            {
                lista = new List<TrabajadorConPlanillaModel>();
            }

            result.data = lista;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetallePlanilla(int trabajadorID, int anio, int mes, int? trabajadorPlanillaID)
        {
            ViewBag.Title = "Detalle del Trabajador";

            var model = _trabajadorServiceFacade.ListarTrabajadoresConPlanilla(anio, mes).First(x => x.trabajadorID == trabajadorID);

            var listaCategoriasPlanillaGeneradas = _planillaServiceFacade.ListarCategoriaPlanillaGeneradaPorTrabajador(trabajadorID, anio, mes);

            CategoriaPlanillaGeneradaParaTrabajadorModel categoriaPlanillaGenerada;

            IEnumerable<ConceptoGeneradoModel> conceptosGenerados;

            if (trabajadorPlanillaID.HasValue)
            {
                categoriaPlanillaGenerada = listaCategoriasPlanillaGeneradas.First(x => x.trabajadorPlanillaID == trabajadorPlanillaID.Value);

                conceptosGenerados = _planillaServiceFacade.ListarConceptosGeneradosPorategoriaYTrabajador(trabajadorPlanillaID.Value);
            }
            else
            {
                categoriaPlanillaGenerada = listaCategoriasPlanillaGeneradas.First();

                conceptosGenerados = _planillaServiceFacade.ListarConceptosGeneradosPorategoriaYTrabajador(categoriaPlanillaGenerada.trabajadorPlanillaID);
            }

            ViewBag.ListaCategorias = new SelectList(listaCategoriasPlanillaGeneradas, "trabajadorPlanillaID", "categoriaPlanillaDesc", trabajadorPlanillaID);

            ViewBag.InformacionCategoriaPlanillaGenerada = categoriaPlanillaGenerada;

            ViewBag.ConceptosGenerados = conceptosGenerados;

            return PartialView("_DetallePlanilla", model);
        }

        [HttpGet]
        public ActionResult DescargaDetallePlanilla(int trabajadorID, int anio, int mes)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _planillaServiceFacade.ObtenerReporteDetallePlanilla(trabajadorID, anio, mes, FormatoArchivo.XLSX);

                return File(fileContent.fileContent, fileContent.contentType, fileContent.fileName);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}