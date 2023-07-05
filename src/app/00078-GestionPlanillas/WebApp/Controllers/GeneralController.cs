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
    public class GeneralController : Controller
    {
        private IPeriodoServiceFacade _periodoServiceFacade;

        public GeneralController()
        {
            _periodoServiceFacade = new PeriodoServiceFacade();
        }

        [HttpGet]
        public JsonResult ObtenerMesesPorAnio(int I_Anio)
        {
            Response response;

            try
            {
                var lista = _periodoServiceFacade.ObtenerComboMeses(I_Anio);

                response = new Response()
                {
                    Success = true,
                    Message = "Consulta correcta.",
                    Result = lista
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
    }
}