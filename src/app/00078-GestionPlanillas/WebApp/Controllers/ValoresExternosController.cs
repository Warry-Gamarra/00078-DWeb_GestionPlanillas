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
        private IValorExternoConceptoServiceFacade _valorExternoConceptoServiceFacade;
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private ITrabajadorServiceFacade _tabajadorServiceFacade;
        private IPlantillaPlanillaServiceFacade _plantillaPlanillaServiceFacade;

        public ValoresExternosController()
        {
            _valorExternoConceptoServiceFacade = new ValorExternoConceptoServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _tabajadorServiceFacade = new TrabajadorServiceFacade();
            _plantillaPlanillaServiceFacade = new PlantillaPlanillaServiceFacade();
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
            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Mantenimiento de Información Externa";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaInformacionExterna(int? anio, int? mes, int? idCategoria)
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

            var trabajador = _tabajadorServiceFacade.ObtenerTrabajador(valorExterno.trabajadorID);

            int? filtro1 = null, filtro2 = null;

            if (trabajador.Vinculo.Equals(Vinculo.AdministrativoPermanente) || trabajador.Vinculo.Equals(Vinculo.AdministrativoContratado))
            {
                filtro1 = trabajador.grupoOcupacionalID;

                filtro2 = trabajador.nivelRemunerativoID;
            }
            else if (trabajador.Vinculo.Equals(Vinculo.DocentePermanente) || trabajador.Vinculo.Equals(Vinculo.DocenteContratado))
            {
                filtro1 = trabajador.categoriaDocenteID;

                filtro2 = trabajador.horasDocenteID;
            }

            var plantillaPlanilla = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanillaPorCategoria(valorExterno.categoriaPlanillaID);

            var esValorFijo = _valorExternoConceptoServiceFacade.EsValorFijo((plantillaPlanilla == null ? 0 : plantillaPlanilla.plantillaPlanillaID.Value), 
                valorExterno.conceptoID, filtro1, filtro2);

            ViewBag.TipoValor = esValorFijo.HasValue ? (esValorFijo.Value ? "Valor Fijo (S/.)" : "Valor Porcentual (%)") : "Tipo de valor sin definir";

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