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
    public class PeriodoService : IPeriodoService
    {
        public List<int> ListarAños()
        {
            var listar = TR_Periodo.GetYears();

            var result = listar
                .OrderBy(x => x.I_Anio)
                .Select(x => x.I_Anio)
                .ToList();

            return result;
        }

        public List<MesDTO> ListarMeses(int I_Anio)
        {
            var lista = TR_Periodo.FindMonthsByYear(I_Anio);

            var result = lista
                .Select(x => Mapper.TR_Periodo_To_MesDTO(x))
                .ToList();

            return result;
        }
    }
}
