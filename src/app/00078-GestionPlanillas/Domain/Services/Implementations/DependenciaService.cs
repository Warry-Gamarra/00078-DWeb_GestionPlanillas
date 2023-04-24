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
        public List<DependenciaDTO> ListarDependencias(bool incluirDeshabilitados = false)
        {
            var lista = TC_Dependencia.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_Dependencia_To_DependenciaDTO(x))
                .OrderBy(x => x.T_DependenciaDesc)
                .ToList();

            return result;
        }
    }
}
