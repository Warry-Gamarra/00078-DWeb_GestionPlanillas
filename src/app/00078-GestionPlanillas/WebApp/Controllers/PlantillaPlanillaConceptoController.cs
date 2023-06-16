using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ServiceFacade.Implementations;
using WebApp.ServiceFacade;
using Domain.Enums;
using Domain.Helpers;
using WebApp.Models;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    public class PlantillaPlanillaConceptoController : Controller
    {
        private ICategoriaPlanillaServiceFacade _categoriaPlanillaServiceFacade;
        private IConceptoServiceFacade _conceptoServiceFacade;
        private IPlantillaPlanillaServiceFacade _plantillaPlanillaServiceFacade;
        private IPlantillaPlanillaConceptoServiceFacade _plantillaPlanillaConceptoServiceFacade;

        private INivelRemunerativoServiceFacade _nivelRemunerativoServiceFacade;
        private IGrupoOcupacionalServiceFacade _grupoOcupacionalServiceFacade;

        private ICategoriaDocenteServiceFacade _categoriaDocenteServiceFacade;
        private IHorasDocenteServiceFacade _horasDocenteServiceFacade;

        public PlantillaPlanillaConceptoController()
        {
            _categoriaPlanillaServiceFacade = new CategoriaPlanillaServiceFacade();
            _conceptoServiceFacade = new ConceptoServiceFacade();
            _plantillaPlanillaServiceFacade = new PlantillaPlanillaServiceFacade();
            _plantillaPlanillaConceptoServiceFacade = new PlantillaPlanillaConceptoServiceFacade();

            _nivelRemunerativoServiceFacade = new NivelRemunerativoServiceFacade();
            _grupoOcupacionalServiceFacade = new GrupoOcupacionalServiceFacade();

            _categoriaDocenteServiceFacade = new CategoriaDocenteServiceFacade();
            _horasDocenteServiceFacade = new HorasDocenteServiceFacade();
        }

        [HttpGet]
        public ActionResult ConceptosAsignados(int id)
        {
            ViewBag.Title = "Conceptos asignados";

            ViewBag.Action = "Actualizar";

            var lista = _plantillaPlanillaConceptoServiceFacade.ListarConceptosAsignados(id);

            var plantilla = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanilla(id);

            ViewBag.PlantillaPlanillaID = id;

            ViewBag.ClasePlanillaDesc = plantilla.clasePlanillaDesc;

            ViewBag.CategoriaPlanillaDesc = plantilla.categoriaPlanillaDesc;

            ViewBag.PlantillaPlanillaDesc = plantilla.plantillaPlanillaDesc;

            ViewBag.EstaHabilitado = plantilla.estaHabilitado;

            return PartialView("_ConceptosAsignados", lista);
        }

        [HttpGet]
        public ActionResult Asignar(int id)
        {
            ViewBag.Title = "Agregar concepto";

            ViewBag.Action = "Registrar";

            var plantilla = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanilla(id);

            ViewBag.ListaConceptos = _conceptoServiceFacade.ListarConceptos(false);

            if (plantilla.categoriaPlanillaID == 1)
            {
                ViewBag.ListaFiltro1 = _grupoOcupacionalServiceFacade.ListarGruposOcupacionales();

                ViewBag.ListaFiltro2 = _nivelRemunerativoServiceFacade.ListarNivelesRemunerativos();
            }
            else if (plantilla.categoriaPlanillaID == 2)
            {
                ViewBag.ListaFiltro1 = _categoriaDocenteServiceFacade.ListarCategoriasDocente();

                ViewBag.ListaFiltro2 = _horasDocenteServiceFacade.ListarHorasDedicacionDocente();
            }
            else
            {
                ViewBag.ListaFiltro1 = new SelectList(new List<SelectListItem>());

                ViewBag.ListaFiltro2 = new SelectList(new List<SelectListItem>());
            }

            var model = new ConceptoAsignadoPlantillaModel()
            {
                plantillaPlanillaID = id,
                esValorFijo = true
            };

            return PartialView("_MantenimientoAsignacionConcepto", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(ConceptoAsignadoPlantillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _plantillaPlanillaConceptoServiceFacade.GrabarPlantillaPlanillaConcepto(Operacion.Registrar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.PlantillaPlanillaID = model.plantillaPlanillaID;

            return PartialView("_MsgRegistrarPlantillaPlanillaConcepto", response);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ViewBag.Title = "Detalle del concepto";

            ViewBag.Action = "Actualizar";

            var model = _plantillaPlanillaConceptoServiceFacade.ObtenerPlantillaPlanillaConcepto(id);

            var plantilla = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanilla(model.plantillaPlanillaID);

            ViewBag.ListaConceptos = _conceptoServiceFacade.ListarConceptos(true);

            if (plantilla.categoriaPlanillaID == 1)
            {
                ViewBag.ListaFiltro1 = _grupoOcupacionalServiceFacade.ListarGruposOcupacionales(selectedItem: model.filtro1);

                ViewBag.ListaFiltro2 = _nivelRemunerativoServiceFacade.ListarNivelesRemunerativos(selectedItem: model.filtro2);
            }
            else if (plantilla.categoriaPlanillaID == 2)
            {
                ViewBag.ListaFiltro1 = _categoriaDocenteServiceFacade.ListarCategoriasDocente(selectedItem: model.filtro1);

                ViewBag.ListaFiltro2 = _horasDocenteServiceFacade.ListarHorasDedicacionDocente(selectedItem: model.filtro2);
            }
            else
            {
                ViewBag.ListaFiltro1 = new SelectList(new List<SelectListItem>());

                ViewBag.ListaFiltro2 = new SelectList(new List<SelectListItem>());
            }

            return PartialView("_MantenimientoAsignacionConcepto", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actualizar(ConceptoAsignadoPlantillaModel model)
        {
            Response response = new Response();

            if (ModelState.IsValid)
            {
                response = _plantillaPlanillaConceptoServiceFacade.GrabarPlantillaPlanillaConcepto(Operacion.Actualizar, model, WebSecurity.CurrentUserId);
            }
            else
            {
                response.Message = "Ocurrió un error.";
            }

            ViewBag.PlantillaPlanillaID = model.plantillaPlanillaID;

            return PartialView("_MsgRegistrarPlantillaPlanillaConcepto", response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CambiarEstado(int rowID, bool estaHabilitado)
        {
            var result = _plantillaPlanillaConceptoServiceFacade.CambiarEstado(rowID, estaHabilitado, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}