using Domain.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            var anio = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Resumen Planilla de Trabajadores";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses(anio);

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
        public ActionResult Generar(int? anio, int? mes, int? idCategoria, bool? busqueda)
        {
            string gridPage = Request.QueryString["grid-page"];

            string gridFilter = Request.QueryString["grid-filter"];

            List<TrabajadorCategoriaPlanillaModel> model;

            var listaAños = _periodoServiceFacade.ObtenerComboAños(selectedItem: anio);

            if (!anio.HasValue)
                anio = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses(I_Anio: anio.Value, selectedItem: mes);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(selectedItem: idCategoria);

            if (anio.HasValue && mes.HasValue)
            {
                if (gridPage == null && gridFilter == null)
                //if (busqueda.HasValue)
                {
                    model = _trabajadorServiceFacade.ListarTrabajadoresCategoriaPlanilla(idCategoria);

                    if (Session["lista"] != null)
                    {
                        Session.Remove("lista");
                    }

                    Session["lista"] = model;
                }
                else
                {
                    model = (List<TrabajadorCategoriaPlanillaModel>)Session["lista"];
                }
            }
            else
            {
                model = new List<TrabajadorCategoriaPlanillaModel>();
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult DeshabilitarTrabajador(int id, bool isChecked)
        {
            Response response;

            try
            {
                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["lista"];

                lista.ForEach(x => {
                    if (x.trabajadorID == id)
                    {
                        x.seleccionado = isChecked;
                    }
                });

                Session.Remove("lista");

                Session["lista"] = lista;

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

                if (Session["lista"] == null)
                {
                    response.Message = "Ha ocurrido un error obteniendo los trabajadores para la planilla. Por favor realice la búsqueda nuevamente.";
                }

                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["lista"];

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