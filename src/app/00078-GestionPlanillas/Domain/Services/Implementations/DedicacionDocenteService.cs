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
    public class DedicacionDocenteService : IDedicacionDocenteService
    {
        public List<DedicacionDocenteDTO> ListarDedicacionDocente(bool incluirDeshabilitados = false, bool? esParaDocenteOrdinario = null)
        {
            var lista = TC_DedicacionDocente.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            if (esParaDocenteOrdinario.HasValue)
            {
                lista = lista.Where(x => x.B_ParaDocenteOrdinario == esParaDocenteOrdinario.Value);
            }

            var result = lista
                .Select(x => Mapper.TC_DedicacionDocente_To_DedicacionDocenteDTO(x))
                .ToList();

            return result;
        }
    }
}
