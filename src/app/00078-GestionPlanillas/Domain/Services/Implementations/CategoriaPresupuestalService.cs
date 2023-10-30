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
    public class CategoriaPresupuestalService : ICategoriaPresupuestalService
    {
        public List<CategoriaPresupuestalDTO> ListarCategoriaPresupuestal(bool incluirDeshabilitados = false)
        {
            var lista = TC_CategoriaPresupuestal.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_CategoriaPresupuestal_To_CategoriaPresupuestalDTO(x))
                .ToList();

            return result;
        }
    }
}
