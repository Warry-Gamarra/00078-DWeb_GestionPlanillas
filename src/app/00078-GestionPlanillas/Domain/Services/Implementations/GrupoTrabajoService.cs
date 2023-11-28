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
    public class GrupoTrabajoService : IGrupoTrabajoService
    {
        public List<GrupoTrabajoDTO> ListarGruposTrabajo(bool incluirDeshabilitados = false)
        {
            var lista = TC_GrupoTrabajo.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_GrupoTrabajo_To_GrupoTrabajoDTO(x))
                .ToList();

            return result;
        }
    }
}
