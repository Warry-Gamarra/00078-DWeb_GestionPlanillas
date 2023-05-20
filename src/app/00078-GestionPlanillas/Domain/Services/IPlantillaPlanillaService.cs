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
    public interface IPlantillaPlanillaService
    {
        Response GrabarPlantillaPlanilla(Operacion operacion, PlantillaPlanillaEntity plantillaPlanillaEntity, int userID);

        List<PlantillaPlanillaDTO> ListarPlantillasPlanilla(bool incluirDeshabilitados = false);

        PlantillaPlanillaDTO ObtenerPlantillaPlanilla(int plantillaPlanillaID);

        Response CambiarEstado(int plantillaPlanillaID, bool estadHabilitado, int userID);
    }
}
