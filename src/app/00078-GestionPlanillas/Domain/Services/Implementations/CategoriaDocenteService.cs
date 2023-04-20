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
        public List<CategoriaDocenteDTO> ListarCategoriasDocente()
        {
            var lista = TC_CategoriaDocente.FindAll()
                .Select(x => Mapper.TC_CategoriaDocente_To_CategoriaDocenteDTO(x))
                .ToList();

            return lista;
        }
    }
}
