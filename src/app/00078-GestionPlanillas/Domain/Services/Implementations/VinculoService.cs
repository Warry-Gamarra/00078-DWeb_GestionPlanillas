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
    public class VinculoService : IVinculoService
    {
        public List<VinculoDTO> ListarVinculos()
        {
            var lista = TC_Vinculo.FindAll()
                .Select(x => Mapper.TC_Vinculo_To_VinculoDTO(x))
                .ToList();

            return lista;
        }
    }
}
