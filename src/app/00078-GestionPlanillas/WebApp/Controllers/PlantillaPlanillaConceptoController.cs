﻿using System;
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

            var plantilla = _plantillaPlanillaServiceFacade.ObtenerPlantillaPlanilla(id);

            return PartialView("_ConceptosAsignados", plantilla);
        }

        [HttpGet]
        public JsonResult ObtenerConceptosAsignados(int id)
        {
            var result = new AjaxResponse();

            var lista = _plantillaPlanillaConceptoServiceFacade.ListarConceptosAsignados(id);

            var listaGrupoOcupacional = _grupoOcupacionalServiceFacade.ListarGruposOcupacionales();

            var listaNivelRemunerativo = _nivelRemunerativoServiceFacade.ListarNivelesRemunerativos();

            var listaCategoriaDocente = _categoriaDocenteServiceFacade.ListarCategoriasDocente();

            var listaHorasDocente = _horasDocenteServiceFacade.ListarHorasDedicacionDocente();

            lista.ForEach(x => {
                if (x.categoriaPlanillaID == 1)
                {
                    if (x.aplicarFiltro1)
                    {
                        x.descFiltro1 = listaGrupoOcupacional.Where(g => g.I_GrupoOcupacionalID == x.filtro1.Value)
                            .First().C_GrupoOcupacionalCod;
                    }

                    if (x.aplicarFiltro2)
                    {
                        x.descFiltro2 = listaNivelRemunerativo.Where(n => n.I_NivelRemunerativoID == x.filtro2.Value)
                            .First().C_NivelRemunerativoCod;
                    }
                }
                

                if (x.categoriaPlanillaID == 2)
                {
                    if (x.aplicarFiltro1)
                    {
                        x.descFiltro1 = listaCategoriaDocente.Where(c => c.I_CategoriaDocenteID == x.filtro1.Value)
                            .First().C_CategoriaDocenteCod;
                    }

                    if (x.aplicarFiltro2)
                    {
                        x.descFiltro2 = listaHorasDocente.Where(h => h.I_HorasDocenteID == x.filtro2.Value)
                            .First().T_DedicacionXHorasCorto;
                    }
                }
            });

            result.data = lista;

            return Json(result, JsonRequestBehavior.AllowGet);
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
                ViewBag.DescFiltro1 = "Grupo Ocupacional";

                ViewBag.ListaFiltro1 = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales();

                ViewBag.DescFiltro2 = "Nivel Remunerativo";

                ViewBag.ListaFiltro2 = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos();
            }
            else if (plantilla.categoriaPlanillaID == 2)
            {
                ViewBag.DescFiltro1 = "Categoría";

                ViewBag.ListaFiltro1 = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente();

                ViewBag.DescFiltro2 = "Dedicación";

                ViewBag.ListaFiltro2 = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente();
            }
            else
            {
                ViewBag.ListaFiltro1 = new SelectList(new List<SelectListItem>());

                ViewBag.ListaFiltro2 = new SelectList(new List<SelectListItem>());
            }

            var listaConceptosIncluidos = _plantillaPlanillaConceptoServiceFacade.ListarConceptosAsignados(plantilla.plantillaPlanillaID.Value)
                .Where(x => x.plantillaPlanillaConceptoID != id && x.estaHabilitado);

            ViewBag.ListaConceptosIncluidos = new SelectList(listaConceptosIncluidos, "plantillaPlanillaConceptoID", "conceptoDesc");

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
                ViewBag.DescFiltro1 = "Grupo Ocupacional";

                ViewBag.ListaFiltro1 = _grupoOcupacionalServiceFacade.ObtenerComboGruposOcupacionales(selectedItem: model.filtro1);

                ViewBag.DescFiltro2 = "Nivel Remunerativo";

                ViewBag.ListaFiltro2 = _nivelRemunerativoServiceFacade.ObtenerComboNivelesRemunerativos(selectedItem: model.filtro2);
            }
            else if (plantilla.categoriaPlanillaID == 2)
            {
                ViewBag.DescFiltro1 = "Categoría";

                ViewBag.ListaFiltro1 = _categoriaDocenteServiceFacade.ObtenerComboCategoriasDocente(selectedItem: model.filtro1);

                ViewBag.DescFiltro2 = "Dedicación";

                ViewBag.ListaFiltro2 = _horasDocenteServiceFacade.ObtenerComboHorasDedicacionDocente(selectedItem: model.filtro2);
            }
            else
            {
                ViewBag.ListaFiltro1 = new SelectList(new List<SelectListItem>());

                ViewBag.ListaFiltro2 = new SelectList(new List<SelectListItem>());
            }

            var listaConceptosIncluidos = _plantillaPlanillaConceptoServiceFacade.ListarConceptosAsignados(plantilla.plantillaPlanillaID.Value)
                .Where(x => x.plantillaPlanillaConceptoID != id && x.estaHabilitado);

            ViewBag.ListaConceptosIncluidos = new SelectList(listaConceptosIncluidos, "plantillaPlanillaConceptoID", "conceptoDesc");

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Eliminar(int id)
        {
            var result = _plantillaPlanillaConceptoServiceFacade.Eliminar(id, WebSecurity.CurrentUserId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}