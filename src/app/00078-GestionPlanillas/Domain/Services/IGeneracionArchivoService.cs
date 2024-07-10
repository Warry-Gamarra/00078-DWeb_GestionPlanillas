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
        FileContent GenerarDescargableListaTrabajadores(List<TrabajadorDTO> data);

        FileContent GenerarDescargableDeLecturaValoresDeConceptos(List<ValorExternoLecturaProcesadoDTO> lista);

        FileContent GenerarDescargableResumenPlanillaTrabajador(IEnumerable<ResumenPlanillaTrabajadorDTO> data);

        FileContent GenerarDescargableResumenPorActividadYDependencia(ReporteResumenPorActividadYDependencia reporte);

        FileContent GenerarDescargableResumenSIAF(ReporteResumenSIAF reporte);

        FileContent GenerarDescargableDetallePlanillaDeTrabajador(TrabajadorDTO trabajador, IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorDTO> listaCategoriasPlanilla,
            List<ConceptoGeneradoDTO> conceptosGenerados);

        FileContent GenerarDescargableDetallePlanillaTrabajadores(ReporteDetallePlanillaTrabajadorDTO reporte);

        FileContent GenerarDescargableDeLecturaCargaDeTrabajadores(List<TrabajadorLecturaProcesadoDTO> lista);

        FileContent GenerarDescargableTrabajadoresAptos(IEnumerable<TrabajadorCategoriaPlanillaDTO> data, int año, string mesDesc);

        FileContent GenerarDescargableInformacionExterna(IEnumerable<ValorExternoConceptoDTO> data, int año, string mesDesc);

        FileContent GenerarDescargableInformacionExterna(IEnumerable<ValorExternoConceptoDTO> data, int año, string mesDesc, int mes);
    }
}
