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
    public class CategoriaDocenteService : ICategoriaDocenteService
    {
        public List<CategoriaDocenteDTO> ListarCategoriasDocente(bool? esParaDocenteOrdinario, bool incluirDeshabilitados = false)
        {
            var lista = TC_CategoriaDocente.FindAll();

            if (esParaDocenteOrdinario.HasValue)
            {
                lista = lista.Where(x => x.B_ParaDocenteOrdinario == esParaDocenteOrdinario.Value);
            }

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result= lista
                .Select(x => Mapper.TC_CategoriaDocente_To_CategoriaDocenteDTO(x))
                .ToList();

            return result;
        }
    }
}
