﻿using Domain.Enums;
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
        private ITrabajadorCategoriaPlanillaServiceFacade _trabajadorCategoriaPlanillaService;
        private IAdministrativoServiceFacade _administrativoServiceFacade;
        private IDocenteServiceFacade _docenteServiceFacade;
        private IEstadoServiceFacade _estadoServiceFacade;
        private IVinculoServiceFacade _vinculoServiceFacade;
        private IRegimenServiceFacade _regimenServiceFacade;
        private ITipoDocumentoServiceFacade _tipoDocumentoServiceFacade;
        private IBancoServiceFacade _bancoServiceFacade;
        private IDependenciaServiceFacade _dependenciaServiceFacade;
        private IAfpServiceFacade _afpServiceFacade;
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IGrupoTrabajoServiceFacade _grupoTrabajoServiceFacade;

        private INivelRemunerativoServiceFacade _nivelRemunerativoServiceFacade;
        private IGrupoOcupacionalServiceFacade _grupoOcupacionalServiceFacade;

        private ICategoriaDocenteServiceFacade _categoriaDocenteServiceFacade;
        private IHorasDocenteServiceFacade _horasDocenteServiceFacade;

        public TrabajadoresController()
        {
            _personaServiceFacade = new PersonaServiceFacade();
            _trabajadorServiceFacade = new TrabajadorServiceFacade();
            _trabajadorCategoriaPlanillaService = new TrabajadorCategoriaPlanillaServiceFacade();
            _administrativoServiceFacade = new AdministrativoServiceFacade();
            _docenteServiceFacade = new DocenteServiceFacade();
            _estadoServiceFacade = new EstadoServiceFacade();
            _vinculoServiceFacade = new VinculoServiceFacade();
            _regimenServiceFacade = new RegimenServiceFacade();
            _tipoDocumentoServiceFacade = new TipoDocumentoServiceFacade();
            _bancoServiceFacade = new BancoServiceFacade();
            _dependenciaServiceFacade = new DependenciaServiceFacade();
            _afpServiceFacade = new AfpServiceFacade();
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _grupoTrabajoServiceFacade = new GrupoTrabajoServiceFacade();

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

            ViewBag.ListaTipoCuentasBancarias = _personaServiceFacade.ObtenerComboTipoCuentasBancarias();

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
                response.Message = "Ocurrió un error.";
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

            ViewBag.ListaTipoCuentasBancarias = _personaServiceFacade.ObtenerComboTipoCuentasBancarias(selectedItem: trabajador.tipoCuentaBancariaID);

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
                response.Message = "Ocurrió un error.";
            }

            return PartialView("_MsgRegistrarTrabajador", response);
        }

        [HttpGet]
        public ActionResult CategoriasPlanillaAsignadas(int id)
        {
            ViewBag.Title = "Categorias planillas asignados";

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            ViewBag.CategoriaPlanillaDesc = _trabajadorCategoriaPlanillaService.ListarCategoriaPlanillaPorTrabajador(id)
                .Where(x => x.esCategoriaPrincipal).First().categoriaPlanillaDesc.ToUpper();

            return PartialView("_CategoriasPlanillaAsignadas", trabajador);
        }

        [HttpGet]
        public JsonResult ObtenerListaTrabajadoresCategoriaPlanilla(int id)
        {
            var result = new AjaxResponse();

            result.data = _trabajadorCategoriaPlanillaService.ListarCategoriaPlanillaPorTrabajador(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AsignarCategoriaPlanilla(int id)
        {
            ViewBag.Title = "Asignar a Planilla";

            ViewBag.Action = "AsignarCategoriaPlanilla";

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(id);

            var categoriaPlanillaID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajador.vinculoID);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(categoriaPlanillaExcluidaID: categoriaPlanillaID);

            ViewBag.ListaGruposTrabajo = _grupoTrabajoServiceFacade.ObtenerComboGruposTrabajo();

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: false);

            var model = new TrabajadorCategoriaPlanillaModel()
            {
                trabajadorID = trabajador.trabajadorID.Value
            };

            return PartialView("_AsignarCategoriaPlanilla", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarCategoriaPlanilla(TrabajadorCategoriaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorCategoriaPlanillaService.GrabarTrabajadorCategoriaPlanilla(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.TrabajadorID = model.trabajadorID;

            return PartialView("_MsgAsignarCategoriaPlanilla", response);
        }

        [HttpGet]
        public ActionResult EditarCategoriaPlanilla(int id)
        {
            ViewBag.Title = "Asignar a Planilla";

            ViewBag.Action = "EditarCategoriaPlanilla";

            var model = _trabajadorCategoriaPlanillaService.ObtenerTrabajadorCategoriaPlanilla(id);

            var trabajador = _trabajadorServiceFacade.ObtenerTrabajador(model.trabajadorID);

            var categoriaPlanillaPrincipalID = (int)_trabajadorCategoriaPlanillaService.ObtenerCategoriaPlanillaSegunVinculo(trabajador.vinculoID);

            ViewBag.ListaCategoriasPlanillas = _categoriaPlanillaServiceFacade.ObtenerComboCategoriasPlanillas(
                categoriaPlanillaExcluidaID: categoriaPlanillaPrincipalID, incluirDeshabilitados: true, selectedItem: model.categoriaPlanillaID);

            ViewBag.ListaGruposTrabajo = _grupoTrabajoServiceFacade.ObtenerComboGruposTrabajo(incluirDeshabilitados: true, selectedItem: model.grupoTrabajoID);

            ViewBag.ListaDependencias = _dependenciaServiceFacade.ObtenerComboDependencias(incluirDeshabilitados: true, selectedItem: model.dependenciaID);

            return PartialView("_AsignarCategoriaPlanilla", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCategoriaPlanilla(TrabajadorCategoriaPlanillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _trabajadorCategoriaPlanillaService.GrabarTrabajadorCategoriaPlanilla(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.TrabajadorID = model.trabajadorID;

            return PartialView("_MsgAsignarCategoriaPlanilla", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstadoCategoriaPlanilla(int rowID, bool estaHabilitado)
        {
            var result = _trabajadorCategoriaPlanillaService.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EliminarCategoriaPlanilla(int id)
        {
            var result = _trabajadorCategoriaPlanillaService.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}