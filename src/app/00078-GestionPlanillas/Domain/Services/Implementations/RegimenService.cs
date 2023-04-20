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
    public class RegimenService : IRegimenService
    {
        public List<RegimenDTO> ListarRegimenes()
        {
            var lista = TC_Regimen.FindAll()
                .Select(x => Mapper.TC_Regimen_To_RegimenDTO(x))
                .ToList();  

            return lista;
        }
    }
}
