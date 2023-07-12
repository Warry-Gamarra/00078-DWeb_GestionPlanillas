using Domain.Entities;
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

        List<MesDTO> ListarMeses(int I_Anio);

        PeriodoDTO ObtenerPeriodo(int anio, int mes);
    }
}
