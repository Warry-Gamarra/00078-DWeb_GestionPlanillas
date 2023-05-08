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

        public ActionResult Index()
        {
            ViewBag.Title = "Resumen Planilla de Trabajadores";

            var lista = _planillaServiceFacade.ListarResumenPlanillaTrabajador();

            return View(lista);
        }

        public ActionResult Generar(int? anio, int? mes, int? idCategoria)
        {
            string page = Request.QueryString["grid-page"];

            List<TrabajadorCategoriaPlanillaModel> model;

            var listaAños = _periodoServiceFacade.ListarAños();

            if (!anio.HasValue)
                anio = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();
            
            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ListarMeses(anio.Value);

            if (anio.HasValue && mes.HasValue)
            {
                if (page == null)
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
        public ActionResult Generar()
        {
            return View();
        }   
    }
}