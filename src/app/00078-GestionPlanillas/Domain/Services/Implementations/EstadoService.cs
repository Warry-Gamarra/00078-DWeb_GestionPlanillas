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
    public class EstadoService : IEstadoService
    {
        public List<EstadoDTO> ListarEstados() 
        {
            var lista = TC_Estado.FindAll()
                .Select(x => Mapper.TC_Estado_To_EstadoDTO(x))
                .ToList();

            return lista;
        }
    }
}
