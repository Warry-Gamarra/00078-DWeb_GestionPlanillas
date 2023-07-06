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

        private ICategoriaDocenteServiceFacade _categoriaDocenteServiceFacade;
        private IHorasDocenteServiceFacade _horasDocenteServiceFacade;

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

            _categoriaDocenteServiceFacade = new CategoriaDocenteServiceFacade();
            _horasDocenteServiceFacade = new HorasDocenteServiceFacade();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Gestión de Trabajadores";

            var lista = _trabajadorServiceFacade.ListarTrabajadores();

            return View(lista);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Title = "Nuevo Trabajador";

            ViewBag.Action = "Registrar";

            ViewBag.ListaEstados = _estadoServiceFacade.ObtenerComboEstados();

            ViewBag.ListaVinculos = _vinculoServiceFacade.ObtenerComboVinculos();

            ViewBag.ListaRegimenes = _regimenServiceFacade.ObtenerComboRegimenes();

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ObtenerComboTipoDocumentos();

            ViewBag.ListaBancos = _bancoServiceFacade.ObtenerComboBancos();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias();

            ViewBag.ListaAfps = _afpServiceFacade.ObtenerComboAfps();

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales();

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos();

            ViewBag.CategoriasDocente = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente();

            ViewBag.HorasDocente = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente();

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

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            ViewBag.ListaEstados = _estadoServiceFacade.ObtenerComboEstados(selectedItem: trabajador.I_EstadoID);

            ViewBag.ListaVinculos = _vinculoServiceFacade.ObtenerComboVinculos(selectedItem: trabajador.I_VinculoID);

            ViewBag.ListaRegimenes = _regimenServiceFacade.ObtenerComboRegimenes(selectedItem: trabajador.I_RegimenID);

            ViewBag.ListaTipoDocumentos = _tipoDocumentoServiceFacade.ObtenerComboTipoDocumentos(selectedItem: trabajador.I_TipoDocumentoID);

            ViewBag.ListaBancos = _bancoServiceFacade.ObtenerComboBancos(selectedItem: trabajador.I_BancoID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(selectedItem: trabajador.I_DependenciaID);

            ViewBag.ListaAfps = _afpServiceFacade.ObtenerComboAfps(selectedItem: trabajador.I_AfpID);

            ViewBag.GruposOcupacionales = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales(selectedItem: trabajador.I_GrupoOcupacionalID);

            ViewBag.NivelesRemunerativos = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos(selectedItem: trabajador.I_NivelRemunerativoID);

            ViewBag.CategoriasDocente = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(selectedItem: trabajador.I_CategoriaDocenteID);

            ViewBag.HorasDocente = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(selectedItem: trabajador.I_HorasDocenteID);

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