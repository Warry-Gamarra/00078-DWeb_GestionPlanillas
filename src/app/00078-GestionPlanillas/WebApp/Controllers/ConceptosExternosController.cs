using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    public class ConceptosExternosController : Controller
    {
        ILecturaArchivo _lecturaArchivo;

        public ConceptosExternosController()
        {
            _lecturaArchivo = new LecturaArchivo();
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

            result.data = _lecturaArchivo.ObtenerListaValoresDeConceptos(inputFile);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CargarArchivo()
        {
            var result = new AjaxResponse();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}