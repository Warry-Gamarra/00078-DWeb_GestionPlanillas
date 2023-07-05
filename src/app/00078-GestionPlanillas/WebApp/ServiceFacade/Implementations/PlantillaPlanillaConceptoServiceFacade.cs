using Domain.Entities;
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

                plantillaPlanillaConceptoEntity.conceptoIncluido = new DataTable();
                plantillaPlanillaConceptoEntity.conceptoIncluido.Columns.Add("I_ID");

                if (model.conceptoReferenciaID != null)
                {
                    foreach (var id in model.conceptoReferenciaID)
                    {
                        plantillaPlanillaConceptoEntity.conceptoIncluido.Rows.Add(id);
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
                .Where(x => x.estaHabilitado && x.esValorFijo);

            var result = new List<SelectListItem>();

            lista.ForEach(x => {
                var item = new SelectListItem()
                {
                    Value = x.plantillaPlanillaConceptoID.ToString(),
                    Text = String.Format("{0} - {1}{2}", x.conceptoCod, x.conceptoDesc,
                        (x.conceptoAbrv != null && x.conceptoAbrv.Length > 0 ? " (" + x.conceptoAbrv + ")" : ""))
                };

                //if (selectedItems!= null && selectedItems.Length > 0)
                //{
                //    item.Selected = selectedItems.Any(i => i == x.plantillaPlanillaConceptoID);
                //}
                
                result.Add(item);
            });

            return new SelectList(result, "Value", "Text", selectedItems);
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