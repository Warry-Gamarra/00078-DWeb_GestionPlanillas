using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IPlanillaServiceFacade
    {
        IEnumerable<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria);

        Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID);

        ReporteResumenPorActividadYDependencia ObtenerReporteResumenPorActividadYDependencia(int año, int mes, int idCategoria);

        FileContent ObtenerReporteResumenPorActividadYDependencia(int año, int mes, int idCategoria, FormatoArchivo formatoArchivo);

        ReporteResumenSIAF ObtenerReporteResumenSIAF(int año, int mes);

        FileContent ObtenerReporteResumenSIAF(int año, int mes, FormatoArchivo formatoArchivo);
    }
}
