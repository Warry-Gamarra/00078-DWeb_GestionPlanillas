using Data.Tables;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class GrupoOcupacionalService : IGrupoOcupacionalService
    {
        public List<GrupoOcupacionalDTO> ListarGruposOcupacionales()
        {
            var lista = TC_GrupoOcupacional.FindAll()
                .Where(x => !x.B_Eliminado)
                .Select(x => Mapper.TC_GrupoOcupacional_To_GrupoOcupacionalDTO(x))
                .ToList();

            return lista;
        }
    }
}
