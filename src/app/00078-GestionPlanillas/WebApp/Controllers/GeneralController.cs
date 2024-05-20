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
                var lista = _periodoServiceFacade.ObtenerComboMesesSegunAño(I_Anio);

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

        public FileResult DescargarFormatoCargaInformacionExterna()
        {
            var nombreArchivo = "Formato carga informacion externa.xlsx";

            var rutaArchivo = Server.MapPath("~/Assets/application/formatos/" + nombreArchivo);

            return File(rutaArchivo, "application/octet-stream", nombreArchivo);
        }

        public FileResult DescargarCatalogoFormatoCargaInformacionExterna()
        {
            var nombreArchivo = "Catalogo del formato de carga de informacion externa.xlsx";

            var rutaArchivo = Server.MapPath("~/Assets/application/formatos/" + nombreArchivo);

            return File(rutaArchivo, "application/octet-stream", nombreArchivo);
        }

        public FileResult ManualUsuario()
        {
            var nombreArchivo = "MCVS-O1-3131 Manual de Usuario.docx";

            var rutaArchivo = Server.MapPath("~/Assets/application/manuales/" + nombreArchivo);

            return File(rutaArchivo, "application/octet-stream", nombreArchivo);
        }

        public FileResult DescargarFormatoCargaMasivaTrabajador()
        {
            var nombreArchivo = "Formato carga trabajadores.xlsx";

            var rutaArchivo = Server.MapPath("~/Assets/application/formatos/" + nombreArchivo);

            return File(rutaArchivo, "application/octet-stream", nombreArchivo);
        }

        public FileResult DescargarCatalogoFormatoCargaMasivaTrabajador()
        {
            var nombreArchivo = "Catalogo del formato de carga de trabajadores.xlsx";

            var rutaArchivo = Server.MapPath("~/Assets/application/formatos/" + nombreArchivo);

            return File(rutaArchivo, "application/octet-stream", nombreArchivo);
        }
    }
}