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
                .OrderByDescending(x => x.I_Anio)
                .Select(x => x.I_Anio)
                .ToList();

            return result;
        }

        public List<MesDTO> ListarMeses(int I_Anio)
        {
            var lista = TR_Periodo.FindMonthsByYear(I_Anio);

            var result = lista
                .OrderBy(x => x.I_Mes)
                .Select(x => Mapper.TR_Periodo_To_MesDTO(x))
                .ToList();

            return result;
        }

        public PeriodoDTO ObtenerPeriodo(int anio, int mes)
        {
            PeriodoDTO periodoDTO;

            var table = TR_Periodo.GetByYearAndMonth(anio, mes);

            if (table == null)
            {
                periodoDTO = null;
            }
            else
            {
                periodoDTO = Mapper.TR_Periodo_To_PeriodoDTO(table);
            }

            return periodoDTO;
        }

        public string ObtenerMesDesc(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "";
            }
        }
    }
}
