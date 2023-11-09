using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
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
        public List<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores(int año, int mes, int idCategoria)
        {
            var lista = USP_S_ListarResumenPlanillaTrabajador.Execute(año, mes, idCategoria)
                .Select(x => Mapper.USP_S_ListarResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(x))
                .ToList();

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

        public List<TotalPlanillaDependenciaDTO> ListarTotalPlanillaPorDependencia(int año, int mes, int idCategoria)
        {
            var lista = USP_S_ListarTotalPlanillaPorDependencia.Execute(año, mes, idCategoria)
                .Select(x => Mapper.USP_S_ListarTotalPlanillaPorDependencia_To_TotalPlanillaDependenciaDTO(x))
                .ToList();

            return lista;
        }

        public IEnumerable<IDictionary<string, object>> ListarResumenSIAF(int año, int mes, int idCategoria)
        {
            var lista = USP_S_ListarResumenSIAF.Execute(año, mes, idCategoria);

            return lista;
        }
    }
}
