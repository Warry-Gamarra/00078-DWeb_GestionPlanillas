using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;

namespace WebApp.Controllers
{
    [Authorize]
    public class GeneralController : Controller
    {
        private IPeriodoServiceFacade _periodoServiceFacade;
        private IPersonaServiceFacade _personaServiceFacade;

        public GeneralController()
        {
            _periodoServiceFacade = new PeriodoServiceFacade();
            _personaServiceFacade = new PersonaServiceFacade();
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

        public JsonResult ObtenerPersonasPorDocIdentidad(int tipoDocumentoID, string numDocumento)
        {
            Response response;

            try
            {
                var lista = _personaServiceFacade.ListarPersonasPorDocIdentidad(tipoDocumentoID, numDocumento);

                response = new Response()
                {
                    Success = true,
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