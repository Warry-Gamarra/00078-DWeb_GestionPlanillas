using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPeriodoService
    {
        List<int> ListarAños();

        List<MesDTO> ListarMesesSegunAnio(int I_Anio);

        PeriodoDTO ObtenerPeriodo(int anio, int mes);

        string ObtenerMesDesc(int mes);

        Response GrabarPeriodo(Operacion operacion, PeriodoEntity periodoEntity, int userID);

        List<PeriodoDTO> ListarPeriodos();

        PeriodoDTO ObtenerPeriodo(int periodoID);

        List<MesDTO> ListarMeses();
    }
}
