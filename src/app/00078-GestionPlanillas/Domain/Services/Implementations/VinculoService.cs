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
        public List<VinculoDTO> ListarVinculos(bool incluirDeshabilitados = false)
        {
            var lista = TC_Vinculo.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Vinculo_To_VinculoDTO(x))
                .ToList();

            return result;
        }
    }
}
