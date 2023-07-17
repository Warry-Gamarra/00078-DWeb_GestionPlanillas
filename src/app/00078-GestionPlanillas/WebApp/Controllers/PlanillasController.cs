using Domain.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    public class PlanillasController : Controller
    {
        private IPlanillaServiceFacade _planillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ITrabajadorServiceFacade _trabajadorServiceFacade;

        public PlanillasController()
        {
            _planillaServiceFacade = new PlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños();

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Resumen Planilla de Trabajadores";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoreConPlanilla(int? anio, int? mes, int? idCategoria)
        {
            var result = new AjaxResponse();

            List<ResumenPlanillaTrabajadorModel> lista;

            if (anio.HasValue && mes.HasValue)
            {
                lista = _planillaServiceFacade.ListarResumenPlanillaTrabajador()
                    .Where(x => x.anio == anio.Value && x.mes == mes.Value)
                    .ToList();

                if (idCategoria.HasValue)
                {
                    lista = lista.Where(x => x.categoriaPlanillaID == idCategoria.Value).ToList();
                }
            }
            else
            {
                lista = new List<ResumenPlanillaTrabajadorModel>();
            }

            result.data = lista;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Generar()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños();

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoresAptos(int? anio, int? mes, int? idCategoria)
        {
            var result = new AjaxResponse();

            List<TrabajadorCategoriaPlanillaModel> listaTrabajadoresAptos;

            if (idCategoria.HasValue)
            {
                listaTrabajadoresAptos = _trabajadorServiceFacade.ListarTrabajadoresCategoriaPlanilla(idCategoria.Value);
            }
            else
            {
                listaTrabajadoresAptos = new List<TrabajadorCategoriaPlanillaModel>();
            }

            if (Session["listaTrabajadoresAptos"] != null)
            {
                Session.Remove("listaTrabajadoresAptos");
            }

            Session["listaTrabajadoresAptos"] = listaTrabajadoresAptos;

            result.data = listaTrabajadoresAptos;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeshabilitarTrabajador(int id, bool isChecked)
        {
            Response response;

            try
            {
                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["listaTrabajadoresAptos"];

                lista.ForEach(x => {
                    if (x.trabajadorID == id)
                    {
                        x.seleccionado = isChecked;
                    }
                });

                Session.Remove("listaTrabajadoresAptos");

                Session["listaTrabajadoresAptos"] = lista;

                response = new Response()
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generar(int anio, int mes, int categoriaPlanillaID)
        {
            Response response ;

            try
            {
                response = new Response();

                if (Session["listaTrabajadoresAptos"] == null)
                {
                    response.Message = "Ha ocurrido un error obteniendo los trabajadores para la planilla. Por favor realice la búsqueda nuevamente.";
                }

                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["listaTrabajadoresAptos"];

                response = _planillaServiceFacade.GenerarPlanilla(
                    lista.Where(x => x.seleccionado).Select(x => x.trabajadorID).ToList(), anio, mes, categoriaPlanillaID, WebSecurity.CurrentUserId);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }   
    }
}