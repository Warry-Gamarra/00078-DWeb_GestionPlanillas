using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ConceptosExternosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Cargar información externa";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ObtenerInformacionArchivo()
        {
            var result = new AjaxResponse();

            result.data = null;

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