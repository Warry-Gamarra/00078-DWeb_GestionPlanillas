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
    public class TrabajadoresController : Controller
    {
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

        public TrabajadoresController()
        {
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
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Consulta";

            var lista = _trabajadorServiceFacade.ListarTrabajadores();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Trabajador";

            ViewBag.Action = "Registrar";

            ViewBag.ListaEstados = _estadoServiceFacade.ListarEstados();

            ViewBag.ListaVinculos = _vinculoServiceFacade.ListarVinculos();

            ViewBag.ListaRegimenes = _regimenServiceFacade.ListarRegimenes();

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ListarTipoDocumentos();

            ViewBag.ListaBancos = _bancoServiceFacade.ListarBancos();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ListarDependencias();

            ViewBag.ListaAfps = _afpServiceFacade.ListarAfps();

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ListarGruposOcupacionales();

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ListarNivelesRemunerativos();

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
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del Trabajador";

            ViewBag.Action = "Actualizar";

            ViewBag.ListaEstados = _estadoServiceFacade.ListarEstados();

            ViewBag.ListaVinculos = _vinculoServiceFacade.ListarVinculos();

            ViewBag.ListaRegimenes = _regimenServiceFacade.ListarRegimenes();

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ListarTipoDocumentos();

            ViewBag.ListaBancos = _bancoServiceFacade.ListarBancos();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ListarDependencias();

            ViewBag.ListaAfps = _afpServiceFacade.ListarAfps();

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ListarGruposOcupacionales();

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ListarNivelesRemunerativos();

            var trabajador = _trabajadorServiceFacade.ListarTrabajadores().Where(x => x.I_TrabajadorID == id).FirstOrDefault();

            //if (trabajador.I_VinculoID == 1)//administrativo
            //{
            //    var administrativo = _administrativoServiceFacade.ListarAdministrativoPorTrabajadorID(trabajador.I_TrabajadorID).FirstOrDefault();

            //    return PartialView("_MantenimientoAdministrativo", administrativo);
            //}
            
            //if (trabajador.I_VinculoID == 4)//docente
            //{
            //    var docente = _docenteServiceFacade.ListarDocentePorTrabajadorID(trabajador.I_TrabajadorID).FirstOrDefault();

            //    return PartialView("_MantenimientoDocente", docente);
            //}

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
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }
    }
}