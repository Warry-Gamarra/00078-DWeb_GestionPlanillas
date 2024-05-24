using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPlanillaService
    {
        IEnumerable<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores(int año, int mes, int idCategoria);

        Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID);

        bool ExistePlanillaTrabajador(int idTrabajador, int año, int mes, int idCategoria);

        ReporteResumenPorActividadYDependencia ObtenerReporteResumenActividadPorDependencia(int año, int mes, int idCategoria);

        ReporteResumenSIAF ObtenerReporteResumenSIAF(int año, int mes);

        IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorDTO> ListarCategoriaPlanillaGeneradaPorTrabajador(int trabajadorID, int año, int mes);

        IEnumerable<ConceptoGeneradoDTO> ListarConceptosGeneradosPorategoriaYTrabajador(int trabajadorPlanillaID);

        ReporteDetallePlanillaTrabajadorDTO ListarDetallePlanillaTrabajadores(int año, int mes, int idCategoria);
    }
}
