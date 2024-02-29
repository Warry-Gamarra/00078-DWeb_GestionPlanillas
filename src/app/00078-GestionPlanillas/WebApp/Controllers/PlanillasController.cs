using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    [Authorize]
    public class PlanillasController : Controller
    {
        private IPlanillaServiceFacade _planillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IPeriodoServiceFacade _periodoServiceFacade;
        private ITrabajadorServiceFacade _trabajadorServiceFacade;
        private ITrabajadorCategoriaPlanillaServiceFacade _trabajadorCategoriPlanillaServiceFacade;

        public PlanillasController()
        {
            _planillaServiceFacade = new PlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _periodoServiceFacade = new PeriodoServiceFacade();
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
            _trabajadorCategoriPlanillaServiceFacade = new TrabajadorCategoriaPlanillaServiceFacade();
        }

        [HttpGet]
        public ActionResult Generar()
        {
            var listaAños = _periodoServiceFacade.ObtenerComboAños(soloAñoConMeses: true);

            var año = (listaAños.Count() > 0) ? int.Parse(listaAños.First().Value) : DateTime.Now.Year;

            ViewBag.Title = "Generar Planillas";

            ViewBag.ListaAños = listaAños;

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMesesSegunAño(año);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas();

            if (Session["listaTrabajadoresAptos"] != null)
            {
                Session.Remove("listaTrabajadoresAptos");
            }

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoresAptos(int? anio, int? mes, int? idCategoria)
        {
            var result = new AjaxResponse();

            List<TrabajadorCategoriaPlanillaModel> listaTrabajadoresAptos;

            if (anio.HasValue && mes.HasValue && idCategoria.HasValue)
            {
                listaTrabajadoresAptos = _trabajadorCategoriPlanillaServiceFacade.ListarTrabajadoresAptos(
                    anio.Value, mes.Value, idCategoria.Value).ToList();
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
        public JsonResult PermitirGeneracionPlanilla(int id, bool isChecked)
        {
            Response response;

            try
            {
                var lista = (List<TrabajadorCategoriaPlanillaModel>)Session["listaTrabajadoresAptos"];

                lista.ForEach(x => {
                    if (x.trabajadorCategoriaPlanillaID == id)
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
                    lista.Where(x => x.seleccionado).Select(x => x.trabajadorCategoriaPlanillaID).ToList(), anio, mes, categoriaPlanillaID, WebSecurity.CurrentUserId);
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