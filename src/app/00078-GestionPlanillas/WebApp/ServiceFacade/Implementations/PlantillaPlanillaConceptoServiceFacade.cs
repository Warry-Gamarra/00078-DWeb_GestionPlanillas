using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}