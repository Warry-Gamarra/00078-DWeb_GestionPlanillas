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
        public List<RegimenDTO> ListarRegimenes(bool incluirDeshabilitados = false)
        {
            var lista = TC_Regimen.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Regimen_To_RegimenDTO(x))
                .ToList();  

            return result;
        }
    }
}
