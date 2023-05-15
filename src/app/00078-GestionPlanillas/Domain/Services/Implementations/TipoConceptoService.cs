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
    public class TipoConceptoService : ITipoConceptoService
    {
        public List<TipoConceptoDTO> ListarTiposConceptos(bool incluirDeshabilitados = false)
        {
            var lista = TC_TipoConcepto.FindAll();

            if (!incluirDeshabilitados)
            {
                lista = lista.Where(x => x.B_Habilitado);
            }

            var result = lista
                .Select(x => Mapper.TC_TipoConcepto_To_TipoConceptoDTO(x))
                .ToList();

            return result;
        }
    }
}
