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
        List<int> ListarAños(bool soloAñoConMeses);

        List<MesDTO> ListarMesesSegunAño(int año);

        PeriodoDTO ObtenerPeriodo(int año, int mes);

        string ObtenerMesDesc(int mes);

        Response GrabarAño(int año);

        Response GrabarPeriodo(Operacion operacion, PeriodoEntity periodoEntity, int userID);

        List<PeriodoDTO> ListarPeriodos();

        PeriodoDTO ObtenerPeriodo(int periodoID);

        List<MesDTO> ListarMeses();

        Response Eliminar(int periodoID, int userID);
    }
}
