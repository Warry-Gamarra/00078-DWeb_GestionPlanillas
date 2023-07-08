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
    public class ValoresExternosController : Controller
    {
        ILecturaArchivo _lecturaArchivo;

        public ValoresExternosController()
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

            try
            {
                if (inputFile == null)
                {
                    throw new NullReferenceException("Debe seleccionar un archivo.");
                }

                result.data = _lecturaArchivo.ObtenerListaValoresDeConceptos(inputFile);

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
            var result = new AjaxResponse();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}