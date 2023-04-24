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
        public List<GrupoOcupacionalDTO> ListarGruposOcupacionales(bool incluirDeshabilitados = false)
        {
            var lista = TC_GrupoOcupacional.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_GrupoOcupacional_To_GrupoOcupacionalDTO(x))
                .ToList();

            return result;
        }
    }
}
