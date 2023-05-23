using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class PlantillaPlanillaConceptoService : IPlantillaPlanillaConceptoService
    {
        public List<ConceptoAsignadoPlantillaDTO> ListarConceptosAsignados(int plantillaPlanillaID)
        {
            var lista = VW_ConceptosAsignados_Plantilla.FindByPlantillaPlanillaID(plantillaPlanillaID)
                .Select(x => Mapper.VW_ConceptosAsignados_Plantilla_To_ConceptoAsignadoPlantillaDTO(x))
                .ToList();
            
            return lista;
        }
    }
}
