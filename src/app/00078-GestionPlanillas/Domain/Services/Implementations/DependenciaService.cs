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
    public class DependenciaService : IDependenciaService
    {
        public List<DependenciaDTO> ListarDependencias()
        {
            var lista = TC_Dependencia.FindAll()
                .Select(x => Mapper.TC_Dependencia_To_DependenciaDTO(x))
                .ToList();

            return lista;
        }
    }
}
