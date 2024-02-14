using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using Domain.Services;
using Domain.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using WebApp.Models;

namespace WebApp.ServiceFacade.Implementations
{
    public class PlanillaServiceFacade : IPlanillaServiceFacade
    {
        private IPlanillaService _planillaService;
        private IPeriodoService _periodoService;
        private object generacionArchivoService;

        public PlanillaServiceFacade()
        {
            _planillaService = new PlanillaService();
            _periodoService = new PeriodoService();
        }

        public IEnumerable<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria)
        {
            var lista = _planillaService.ListarResumenPlanillaTrabajadores(año, mes, idCategoria)
                .Select(x => Mapper.ResumenPlanillaTrabajadorDTO_To_ResumenPlanillaTrabajadorModel(x));

            return lista;
        }

        public FileContent ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            IEnumerable<ResumenPlanillaTrabajadorDTO> data;
            FileContent fileContent;

            try
            {
                data = _planillaService.ListarResumenPlanillaTrabajadores(año, mes, idCategoria);

                generacionArchivoService = new GeneracionArchivoExcelService();

                fileContent = generacionArchivoService.GenerarExcelResumenPlanillaTrabajador(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID)
        {
            Response response;

            try
            {
                response = _planillaService.GenerarPlanilla(trabajadores, año, mes, categoriaPlanillaID, userID);
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message
                };
            }

            return response;
        }

        public ReporteResumenPorActividadYDependencia ObtenerReporteResumenPorActividadYDependencia(int año, int mes, int idCategoria)
        {
            return _planillaService.ObtenerReporteResumenActividadPorDependencia(año, mes, idCategoria);
        }

        public FileContent ObtenerReporteResumenPorActividadYDependencia(int año, int mes, int idCategoria, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            ReporteResumenPorActividadYDependencia reporte;
            FileContent fileContent;

            try
            {
                reporte = _planillaService.ObtenerReporteResumenActividadPorDependencia(año, mes, idCategoria);

                generacionArchivoService = new GeneracionArchivoExcelService();

                fileContent = generacionArchivoService.GenerarExcelResumenPorActividadYDependencia(reporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public ReporteResumenSIAF ObtenerReporteResumenSIAF(int año, int mes)
        {
            return _planillaService.ObtenerReporteResumenSIAF(año, mes);
        }

        public FileContent ObtenerReporteResumenSIAF(int año, int mes, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            ReporteResumenSIAF reporte;
            FileContent fileContent;

            try
            {
                reporte = _planillaService.ObtenerReporteResumenSIAF(año, mes);

                generacionArchivoService = new GeneracionArchivoExcelService();

                fileContent = generacionArchivoService.GenerarExcelResumenSIAF(reporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorModel> ListarCategoriaPlanillaGeneradaPorTrabajador(int trabajadorID, int año, int mes)
        {
            var lista = _planillaService.ListarCategoriaPlanillaGeneradaPorTrabajador(trabajadorID, año, mes)
                .Select(x => Mapper.CategoriaPlanillaGeneradaParaTrabajadorDTO_To_CategoriaPlanillaGeneradaParaTrabajadorModel(x));

            return lista;
        }

        public IEnumerable<ConceptoGeneradoModel> ListarConceptosGeneradosPorategoriaYTrabajador(int trabajadorPlanillaID)
        {
            var lista = _planillaService.ListarConceptosGeneradosPorategoriaYTrabajador(trabajadorPlanillaID)
                .Select(x => Mapper.ConceptoGeneradoDTO_To_ConceptoGeneradoModel(x));

            return lista;
        }
    }
}
