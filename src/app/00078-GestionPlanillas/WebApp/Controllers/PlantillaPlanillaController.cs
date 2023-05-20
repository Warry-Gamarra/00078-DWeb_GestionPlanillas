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
    public class PlantillaPlanillaController : Controller
    {
        private IPlantillaPlanillaServiceFacade _plantillaPlanillaServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;

        public PlantillaPlanillaController()
        {
            _plantillaPlanillaServiceFacade = new PlantillaPlanillaServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
        }

        [HttpGet]
        public JsonResult ObtenerListaPlantillasPlanilla()
        {
            var result = new AjaxResponse();

            result.data = _plantillaPlanillaServiceFacade.ListarPlantillasPlanilla();

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nueva Plantilla";

            ViewBag.Action = "Registrar";

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ListarCategoriasPlanillas();

            var concepto = new PlantillaPlanillaModel();

            return PartialView("_MantenimientoPlantillaPlanilla", concepto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(PlantillaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _plantillaPlanillaServiceFacade.GrabarPlantillaPlanilla(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarPlantillaPlanilla", response);
        }
    }
}