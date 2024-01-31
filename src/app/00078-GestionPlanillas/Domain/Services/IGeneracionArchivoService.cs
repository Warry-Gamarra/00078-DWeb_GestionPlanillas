using Domain.Entities;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGeneracionArchivoService
    {
        FileContent GenerarExcelDeLecturaValoresDeConceptos(List<ValorExternoLecturaProcesadoDTO> lista);

        FileContent GenerarExcelResumenPlanillaTrabajador(IEnumerable<ResumenPlanillaTrabajadorDTO> data);

        FileContent GenerarExcelResumenPorActividadYDependencia(ReporteResumenPorActividadYDependencia reporte);

        FileContent GenerarExcelResumenSIAF(ReporteResumenSIAF reporte);
    }
}
