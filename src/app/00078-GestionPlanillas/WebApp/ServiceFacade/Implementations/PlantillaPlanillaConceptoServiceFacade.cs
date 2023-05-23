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
            
        public List<ConceptoAsignadoPlantillaModel> ListarConceptosAsignados(int plantillaPlanillaID)
        {
            var lista = _plantillaPlanillaConceptoService.ListarConceptosAsignados(plantillaPlanillaID)
                .Select(x => Mapper.ConceptoAsignadoPlantillaDTO_To_ConceptoAsignadoPlantillaModel(x))
                .ToList();

            return lista;
        }
    }
}