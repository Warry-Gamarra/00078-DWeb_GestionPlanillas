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
    [Authorize]
    public class PeriodosController : Controller
    {
        private IPeriodoServiceFacade _periodoServiceFacade;

        public PeriodosController()
        {
            _periodoServiceFacade = new PeriodoServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Configuración de Periodos";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaPeriodos()
        {
            var result = new AjaxResponse();

            result.data = _periodoServiceFacade.ListarPeriodos();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Periodo";

            ViewBag.Action = "Registrar";

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses();

            var periodo = new PeriodoModel()
            {
                anio = DateTime.Now.Year
            };

            return PartialView("_MantenimientoPeriodo", periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PeriodoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _periodoServiceFacade.GrabarPeriodo(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarPeriodo", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Periodo";

            ViewBag.Action = "Actualizar";

            var periodo = _periodoServiceFacade.ObtenerPeriodo(id);

            ViewBag.ListaMeses = _periodoServiceFacade.ObtenerComboMeses(selectedItem: periodo.mes);

            return PartialView("_MantenimientoPeriodo", periodo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(PeriodoModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _periodoServiceFacade.GrabarPeriodo(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarPeriodo", response);
        }
    }
}