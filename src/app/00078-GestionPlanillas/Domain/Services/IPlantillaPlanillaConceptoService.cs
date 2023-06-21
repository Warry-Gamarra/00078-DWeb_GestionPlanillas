using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPlantillaPlanillaConceptoService
    {
        Response GrabarPlantillaPlanillaConcepto(Operacion operacion, PlantillaPlanillaConceptoEntity conceptoEntity, int userID);

        List<ConceptoAsignadoPlantillaDTO> ListarConceptosAsignados(int plantillaPlanillaID);

        ConceptoAsignadoPlantillaDTO ObtenerPlantillaPlanillaConcepto(int plantillaPlanillaConceptoID);

        Response CambiarEstado(int plantillaPlanillaConceptoID, bool estadHabilitado, int userID);

        Response Eliminar(int plantillaPlanillaConceptoID, int userID);
    }
}
