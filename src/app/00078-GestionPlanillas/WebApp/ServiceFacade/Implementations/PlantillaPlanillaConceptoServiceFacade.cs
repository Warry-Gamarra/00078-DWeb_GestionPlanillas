﻿using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class PlantillaPlanillaConceptoServiceFacade : IPlantillaPlanillaConceptoServiceFacade
    {
        IPlantillaPlanillaConceptoService _plantillaPlanillaConceptoService;

        public PlantillaPlanillaConceptoServiceFacade()
        {
            _plantillaPlanillaConceptoService = new PlantillaPlanillaConceptoService();
        }

        public Response GrabarPlantillaPlanillaConcepto(Operacion operacion, ConceptoAsignadoPlantillaModel model, int userID)
        {
            Response response;

            try
            {
                var plantillaPlanillaConceptoEntity = new PlantillaPlanillaConceptoEntity()
                {
                    plantillaPlanillaConceptoID = model.plantillaPlanillaConceptoID,
                    plantillaPlanillaID = model.plantillaPlanillaID,
                    conceptoID = model.conceptoID,
                    esValorFijo = model.esValorFijo,
                    valorEsExterno = model.valorEsExterno,
                    valorConcepto = model.valorConcepto,
                    aplicarFiltro1 = model.aplicarFiltro1,
                    filtro1 = model.filtro1,
                    aplicarFiltro2 = model.aplicarFiltro2,
                    filtro2 = model.filtro2
                };

                plantillaPlanillaConceptoEntity.conceptosReferencia = new DataTable();
                plantillaPlanillaConceptoEntity.conceptosReferencia.Columns.Add("I_ID");

                if (model.conceptosReferenciaID != null)
                {
                    foreach (var id in model.conceptosReferenciaID)
                    {
                        plantillaPlanillaConceptoEntity.conceptosReferencia.Rows.Add(id);
                    }
                }

                response = _plantillaPlanillaConceptoService.GrabarPlantillaPlanillaConcepto(operacion, plantillaPlanillaConceptoEntity, userID);

            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public List<ConceptoAsignadoPlantillaModel> ListarConceptosAsignados(int plantillaPlanillaID)
        {
            var lista = _plantillaPlanillaConceptoService.ListarConceptosAsignados(plantillaPlanillaID)
                .Select(x => Mapper.ConceptoAsignadoPlantillaDTO_To_ConceptoAsignadoPlantillaModel(x))
                .ToList();

            return lista;
        }

        public SelectList ObtenerComboConceptosAsignados(int plantillaPlanillaID, int[] selectedItems = null)
        {
            var lista = _plantillaPlanillaConceptoService.ListarConceptosAsignados(plantillaPlanillaID)
                .Where(x => x.estaHabilitado && x.esValorFijo)
                .GroupBy(x => new { x.conceptoID, x.conceptoCod, x.conceptoDesc, x.conceptoAbrv });

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.Key.conceptoID.ToString(),
                    Text = String.Format("{0} - {1}{2}", x.Key.conceptoCod, x.Key.conceptoDesc,
                        (x.Key.conceptoAbrv != null && x.Key.conceptoAbrv.Length > 0 ? " (" + x.Key.conceptoAbrv + ")" : ""))
                };

                result.Add(item);
            });

            if (selectedItems != null && selectedItems.Length > 0)
            {
                return new SelectList(result, "Value", "Text", selectedItems);
            }
            else
            {
                return new SelectList(result, "Value", "Text");
            }
        }

        public ConceptoAsignadoPlantillaModel ObtenerPlantillaPlanillaConcepto(int plantillaPlanillaConceptoID)
        {
            ConceptoAsignadoPlantillaModel plantillaPlanillaConceptoModel;

            var plantillaPlanillaConceptoDTO = _plantillaPlanillaConceptoService.ObtenerPlantillaPlanillaConcepto(plantillaPlanillaConceptoID);

            if (plantillaPlanillaConceptoDTO == null)
            {
                plantillaPlanillaConceptoModel = null;
            }
            else
            {
                plantillaPlanillaConceptoModel = Mapper.ConceptoAsignadoPlantillaDTO_To_ConceptoAsignadoPlantillaModel(plantillaPlanillaConceptoDTO);

                plantillaPlanillaConceptoModel.conceptosReferenciaID = _plantillaPlanillaConceptoService.ListarConceptosReferencia(plantillaPlanillaConceptoID)
                    .Select(x => x.conceptoReferenciaID).ToArray();
            }

            return plantillaPlanillaConceptoModel;
        }

        public Response CambiarEstado(int plantillaPlanillaConceptoID, bool estaHabilitado, int userID)
        {
            var result = _plantillaPlanillaConceptoService.CambiarEstado(plantillaPlanillaConceptoID, estaHabilitado, userID);

            return result;
        }

        public Response Eliminar(int plantillaPlanillaConceptoID, int userID)
        {
            var result = _plantillaPlanillaConceptoService.Eliminar(plantillaPlanillaConceptoID, userID);

            return result;
        }

        public List<ConceptoReferenciaModel> ListarConceptosReferencia(int plantillaPlanillaConceptoID)
        {
            var lista = _plantillaPlanillaConceptoService.ListarConceptosReferencia(plantillaPlanillaConceptoID)
                .Select(x => Mapper.ConceptoReferenciaDTO_To_ConceptoReferenciaModel(x))
                .ToList();

            return lista;
        }
    }
}