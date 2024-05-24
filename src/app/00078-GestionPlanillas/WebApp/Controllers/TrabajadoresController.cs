using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ServiceFacade;
using WebApp.ServiceFacade.Implementations;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    [Authorize]
    public class TrabajadoresController : Controller
    {
        private IPersonaServiceFacade _personaServiceFacade;
        private ITrabajadorServiceFacade _trabajadorServiceFacade;
        private IAdministrativoServiceFacade _administrativoServiceFacade;
        private IDocenteServiceFacade _docenteServiceFacade;
        private IEstadoServiceFacade _estadoServiceFacade;
        private IVinculoServiceFacade _vinculoServiceFacade;
        private IRegimenServiceFacade _regimenServiceFacade;
        private ITipoDocumentoServiceFacade _tipoDocumentoServiceFacade;
        private IBancoServiceFacade _bancoServiceFacade;
        private IDependenciaServiceFacade _dependenciaServiceFacade;
        private IAfpServiceFacade _afpServiceFacade;

        private INivelRemunerativoServiceFacade _nivelRemunerativoServiceFacade;
        private IGrupoOcupacionalServiceFacade _grupoOcupacionalServiceFacade;

        private ICategoriaDocenteServiceFacade _categoriaDocenteServiceFacade;
        private IHorasDocenteServiceFacade _horasDocenteServiceFacade;

        public TrabajadoresController()
        {
            _personaServiceFacade = new PersonaServiceFacade();
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
            _administrativoServiceFacade = new AdministrativoServiceFacade();
            _docenteServiceFacade = new DocenteServiceFacade();
            _estadoServiceFacade = new EstadoServiceFacade();
            _vinculoServiceFacade = new VinculoServiceFacade();
            _regimenServiceFacade = new RegimenServiceFacade();
            _tipoDocumentoServiceFacade = new TipoDocumentoServiceFacade();
            _bancoServiceFacade = new BancoServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
            _afpServiceFacade = new AfpServiceFacade();

            _nivelRemunerativoServiceFacade = new NivelRemunerativoServiceFacade();
            _grupoOcupacionalServiceFacade = new GrupoOcupacionalServiceFacade();

            _categoriaDocenteServiceFacade = new CategoriaDocenteServiceFacade();
            _horasDocenteServiceFacade = new HorasDocenteServiceFacade();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Gestión de Trabajadores";

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadores()
        {
            var result = new AjaxResponse();

            result.data = _trabajadorServiceFacade.ListarTrabajadores();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DescargarListaTrabajadores()
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _trabajadorServiceFacade.ListarTrabajadores(FormatoArchivo.XLSX);

                return File(fileContent.fileContent, fileContent.contentType, fileContent.fileName);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Trabajador";

            ViewBag.Action = "Registrar";

            ViewBag.ListaEstados = _estadoServiceFacade.ObtenerComboEstados();

            ViewBag.ListaVinculos = _vinculoServiceFacade.ObtenerComboVinculos();

            ViewBag.ListaRegimenes = _regimenServiceFacade.ObtenerComboRegimenes();

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ObtenerComboTipoDocumentos();

            ViewBag.ListaSexos = _personaServiceFacade.ObtenerComboSexos();

            ViewBag.ListaBancos = _bancoServiceFacade.ObtenerComboBancos();

            ViewBag.ListaTipoCuentasBancarias = _bancoServiceFacade.ObtenerComboTipoCuentasBancarias();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias();

            ViewBag.ListaAfps = _afpServiceFacade.ObtenerComboAfps();

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales();

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos();

            ViewBag.CategoriasDocenteOrdinario = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(true);

            ViewBag.HorasDocenteOrdinario = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(true);

            ViewBag.CategoriasDocenteContratado = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(false);

            ViewBag.HorasDocenteContratado = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(false);

            ViewBag.PermitirEdicionVinculo = true;

            var trabajador = new TrabajadorModel();

            return PartialView("_MantenimientoTrabajador", trabajador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(TrabajadorModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorServiceFacade.GrabarTrabajador(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                string details = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        details += "<li>" + error.ErrorMessage + "</li>";
                    }
                }

                response.Message = details;
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Trabajador";

            ViewBag.Action = "Actualizar";

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            ViewBag.ListaEstados = _estadoServiceFacade.ObtenerComboEstados(selectedItem: trabajador.estadoID);

            ViewBag.ListaVinculos = _vinculoServiceFacade.ObtenerComboVinculos(selectedItem: trabajador.vinculoID);

            ViewBag.ListaRegimenes = _regimenServiceFacade.ObtenerComboRegimenes(selectedItem: trabajador.regimenID);

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ObtenerComboTipoDocumentos(selectedItem: trabajador.tipoDocumentoID);

            ViewBag.ListaSexos = _personaServiceFacade.ObtenerComboSexos(selectedItem: trabajador.sexoID);

            ViewBag.ListaBancos = _bancoServiceFacade.ObtenerComboBancos(selectedItem: trabajador.bancoID);

            ViewBag.ListaTipoCuentasBancarias = _bancoServiceFacade.ObtenerComboTipoCuentasBancarias(selectedItem: trabajador.tipoCuentaBancariaID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: true, selectedItem: trabajador.dependenciaID);

            ViewBag.ListaAfps = _afpServiceFacade.ObtenerComboAfps(selectedItem: trabajador.afpID);

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales(selectedItem: trabajador.grupoOcupacionalID);

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos(selectedItem: trabajador.nivelRemunerativoID);

            ViewBag.CategoriasDocenteOrdinario = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(true, selectedItem: trabajador.categoriaDocenteID);

            ViewBag.HorasDocenteOrdinario = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(true, selectedItem: trabajador.horasDocenteID);

            ViewBag.CategoriasDocenteContratado = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(false, selectedItem: trabajador.categoriaDocenteID);

            ViewBag.HorasDocenteContratado = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(false, selectedItem: trabajador.horasDocenteID);

            ViewBag.PermitirEdicionVinculo = false;

            return PartialView("_MantenimientoTrabajador", trabajador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(TrabajadorModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorServiceFacade.GrabarTrabajador(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                string details = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        details += "<li>" + error.ErrorMessage + "</li>";
                    }
                }

                response.Message = details;
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ViewBag.Title = "Carga masiva";

            return PartialView("_CargaMasiva");
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

                result.data = _trabajadorServiceFacade.ObtenerListaTrabajadores(inputFile);

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
            var result = _trabajadorServiceFacade.GrabarValoresExternos(fileName, WebSecurity.CurrentUserId);

            var response = new AjaxResponse()
            {
                success = result.Success,
                message = result.Message
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DescargarResultadoLectura(string fileName)
        {
            FileContent fileContent;
            string errorMessage;

            try
            {
                fileContent = _trabajadorServiceFacade.ObtenerResultadoLectura(FormatoArchivo.XLSX, fileName);

                return File(fileContent.fileContent, fileContent.contentType, fileContent.fileName);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}