using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Domain.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class PlanillaService : IPlanillaService
    {
        private IPeriodoService _periodoService;

        public PlanillaService()
        {
            _periodoService = new PeriodoService();
        }

        public IEnumerable<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores(int año, int mes, int idCategoria)
        {
            var lista = USP_S_ListarResumenPlanillaTrabajador.Execute(año, mes, idCategoria)
                .Select(x => Mapper.USP_S_ListarResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(x));

            return lista;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("I_TrabajadorID");

            trabajadores.ForEach(x => {
                dataTable.Rows.Add(x);
            });

            var generarPlanilla = new USP_I_GenerarPlanilla_Docente_Administrativo()
            {
                Tbl_Trabajador = dataTable,
                I_Anio = año,
                I_Mes = mes,
                I_CategoriaPlanillaID = categoriaPlanillaID,
                I_UserID = userID
            };

            var result = generarPlanilla.Execute();

            return Mapper.Result_To_Response(result);
        }

        public bool ExistePlanillaTrabajador(int idTrabajador, int año, int mes, int idCategoria)
        {
            try
            {
                var planillaTrabajador = USP_S_ListarResumenPlanillaTrabajador.Execute(año, mes, idCategoria)
                    .Where(x => x.I_TrabajadorID == idTrabajador)
                    .FirstOrDefault();

                bool existePlanilla = (planillaTrabajador != null ? true : false);

                return existePlanilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteResumenPorActividadYDependencia ObtenerReporteResumenActividadPorDependencia(int año, int mes, int idCategoria)
        {
            var lista = USP_S_ListarResumenPorActividadYDependencia.Execute(año, mes, idCategoria)
                .Select(x => Mapper.USP_S_ListarResumenPorActividadYDependencia_To_ResumenPorActividadYDependenciaDTO(x));

            return new ReporteResumenPorActividadYDependencia(año, _periodoService.ObtenerMesDesc(mes), "-", lista);
        }

        public ReporteResumenSIAF ObtenerReporteResumenSIAF(int año, int mes)
        {
            var admContratadoResult = USP_S_ListarResumenSIAF.Execute(año, mes, (int)CategoriaPlanilla.HaberesAdministrativo, (int)Vinculo.AdministrativoContratado);

            var resumenAdmContratado = new ResumenSIAFDTO("ADMINISTRATIVO CONTRATADO", admContratadoResult.cabecera, admContratadoResult.detalle, "Total Adm Contr");

            var admPermanenteResult = USP_S_ListarResumenSIAF.Execute(año, mes, (int)CategoriaPlanilla.HaberesAdministrativo, (int)Vinculo.AdministrativoPermanente);

            var resumenAdmPermanente = new ResumenSIAFDTO("ADMINISTRATIVO PERMANENTE", admPermanenteResult.cabecera, admPermanenteResult.detalle, "Total Adm Perman");

            var docPermanenteResult = USP_S_ListarResumenSIAF.Execute(año, mes, (int)CategoriaPlanilla.HaberesDocente, (int)Vinculo.DocentePermanente);

            var resumenDocPermanente = new ResumenSIAFDTO("DOCENTE PERMANENTE", docPermanenteResult.cabecera, docPermanenteResult.detalle, "Total Doc Perman");

            var listaResumenes = new List<ResumenSIAFDTO>();

            listaResumenes.Add(resumenAdmContratado);

            listaResumenes.Add(resumenAdmPermanente);

            listaResumenes.Add(resumenDocPermanente);

            return new ReporteResumenSIAF(año, _periodoService.ObtenerMesDesc(mes), listaResumenes);
        }

        public IEnumerable<CategoriaPlanillaGeneradaParaTrabajadorDTO> ListarCategoriaPlanillaGeneradaPorTrabajador(int trabajadorID, int año, int mes)
        {
            var lista = USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador.Execute(trabajadorID, año, mes)
                .Select(x => Mapper.USP_S_ListarCategoriaPlanillaGeneradaPorTrabajador_To_CategoriaPlanillaGeneradaParaTrabajadorModel(x));

            return lista;
        }

        public IEnumerable<ConceptoGeneradoDTO> ListarConceptosGeneradosPorategoriaYTrabajador(int trabajadorPlanillaID)
        {
            var lista = USP_S_ListarConceptosGeneradosPorategoriaYTrabajador.Execute(trabajadorPlanillaID)
                .Select(x => Mapper.USP_S_ListarConceptosGeneradosPorategoriaYTrabajador_To_ConceptoGeneradoDTO(x));

            return lista;
        }
    }
}
