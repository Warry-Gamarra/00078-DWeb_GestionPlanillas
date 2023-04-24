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
    public class AfpService : IAfpService
    {
        public List<AfpDTO> ListarAfps(bool incluirDeshabilitados = false)
        {
            var lista = TC_Afp.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Afp_To_AfpDTO(x))
                .ToList();

            return result;
        }
    }
}
