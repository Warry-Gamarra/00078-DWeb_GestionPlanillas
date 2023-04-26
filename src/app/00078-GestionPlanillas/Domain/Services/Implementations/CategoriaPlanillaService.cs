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
    public class CategoriaPlanillaService : ICategoriaPlanillaService
    {
        public List<CategoriaPlanillaDTO> ListarCategoriasPlanillas(bool incluirDeshabilitados = false)
        {
            var lista = TC_CategoriaPlanilla.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_CategoriaPlanilla_To_CategoriaPlanillaDTO(x))
                .ToList();

            return result;
        }
    }
}
