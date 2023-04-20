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
    public class BancoService : IBancoService
    {
        public List<BancoDTO> ListarBancos()
        {
            var lista = TC_Banco.FindAll()
                .Select(x => Mapper.TC_Banco_To_BancoDTO(x))
                .ToList();
            
            return lista;
        }
    }
}
