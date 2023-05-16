using Data.Tables;
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
    public class PlantillaPlanillaService : IPlantillaPlanillaService
    {
        public List<PlantillaPlanillaDTO> ListarPlantillasPlanilla(bool incluirDeshabilitados = false)
        {
            var lista = VW_PlantillaPlanilla.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.VW_PlantillaPlanilla_To_PlantillaPlanillaDTO(x))
                .ToList();

            return result;
        }
    }
}
