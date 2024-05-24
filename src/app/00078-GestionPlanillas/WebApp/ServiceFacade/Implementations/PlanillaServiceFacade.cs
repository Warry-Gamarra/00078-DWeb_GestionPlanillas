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
        private ITrabajadorService _trabajadorService;

        public PlanillaServiceFacade()
        {
            _planillaService = new PlanillaService();
            _trabajadorService = new TrabajadorService();
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

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableResumenPlanillaTrabajador(data);
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

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableResumenPorActividadYDependencia(reporte);
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

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableResumenSIAF(reporte);
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

        public FileContent ObtenerReporteDetallePlanillaDeTrabajador(int trabajadorID, int año, int mes, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            TrabajadorDTO trabajador;
            IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorDTO> listaCategoriasPlanilla;
            List<ConceptoGeneradoDTO> conceptosGenerados;
            FileContent fileContent;

            try
            {
                trabajador = _trabajadorService.ObtenerTrabajador(trabajadorID);

                listaCategoriasPlanilla = _planillaService.ListarCategoriaPlanillaGeneradaPorTrabajador(trabajadorID, año, mes);

                conceptosGenerados = new List<ConceptoGeneradoDTO>();

                foreach (var item in listaCategoriasPlanilla)
                {
                    conceptosGenerados.AddRange(_planillaService.ListarConceptosGeneradosPorategoriaYTrabajador(item.trabajadorPlanillaID));
                }

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableDetallePlanillaDeTrabajador(trabajador, listaCategoriasPlanilla, conceptosGenerados);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }

        public FileContent ObtenerReporteDetallePlanillaTrabajadores(int año, int mes, int idCategoria, FormatoArchivo formatoArchivo)
        {
            IGeneracionArchivoService generacionArchivoService;
            ReporteDetallePlanillaTrabajadorDTO reporte;
            FileContent fileContent;

            try
            {
                reporte = _planillaService.ListarDetallePlanillaTrabajadores(año, mes, idCategoria);

                generacionArchivoService = FileManagement.GetGeneracionArchivoService(formatoArchivo);

                fileContent = generacionArchivoService.GenerarDescargableDetallePlanillaTrabajadores(reporte);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileContent;
        }
    }
}
