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
    public class NivelRemunerativoService : INivelRemunerativoService
    {
        public List<NivelRemunerativoDTO> ListarNivelesRemunerativos()
        {
            var lista = TC_NivelRemunerativo.FindAll()
                .Where(x => !x.B_Eliminado)
                .Select(x => Mapper.TC_NivelRemunerativo_To_NivelRemunerativoDTO(x))
                .ToList();

            return lista;
        }
    }
}
