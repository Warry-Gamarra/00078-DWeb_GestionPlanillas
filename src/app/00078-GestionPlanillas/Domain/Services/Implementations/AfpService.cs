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
        public List<AfpDTO> ListarAfps()
        {
            var lista = TC_Afp.FindAll()
                .Where(x => !x.B_Eliminado)
                .Select(x => Mapper.TC_Afp_To_AfpDTO(x))
                .ToList();

            return lista;
        }
    }
}
