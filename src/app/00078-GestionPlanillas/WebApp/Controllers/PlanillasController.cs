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

        public ActionResult Index(int? anio, int? mes, int? idCategoria)
        {
            var listaAños = _periodoServiceFacade.ListarAños();

            if (!anio.HasValue)
                anio = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Resumen Planilla de Trabajadores";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ListarMeses(anio.Value);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

            List<ResumenPlanillaTrabajadorModel> model;

            if (anio.HasValue && mes.HasValue)
            {
                model = _planillaServiceFacade.ListarResumenPlanillaTrabajador()
                    .Where(x => x.I_Anio == anio.Value && x.I_Mes == mes.Value)
                    .ToList();

                if (idCategoria.HasValue)
                {
                    model = model.Where(x => x.I_CategoriaPlanillaID == idCategoria.Value).ToList();
                }
            }
            else
            {
                model = new List<ResumenPlanillaTrabajadorModel>();
            }

            return View(model);
        }

        public ActionResult Generar(int? anio, int? mes, int? idCategoria, bool? busqueda)
        {
            string gridPage = Request.QueryString["grid-page"];

            string gridFilter = Request.QueryString["grid-filter"];

            List<TrabajadorCategoriaPlanillaModel> model;

            var listaAños = _periodoServiceFacade.ListarAños();

            if (!anio.HasValue)
                anio = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ListarMeses(anio.Value);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

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

        public JsonResult DeshabilitarTrabajador(int id, bool isChecked)
        {
            Response response;

            try
            {
                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["lista"];

                lista.ForEach(x => {
                    if (x.I_TrabajadorID == id)
                    {
                        x.B_Checked = isChecked;
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
        public ActionResult Generar(int I_Anio, int I_Mes, int I_CategoriaPlanillaID)
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
                    lista.Where(x => x.B_Checked).Select(x => x.I_TrabajadorID).ToList(), I_Anio, I_Mes, I_CategoriaPlanillaID, WebSecurity.CurrentUserId);
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