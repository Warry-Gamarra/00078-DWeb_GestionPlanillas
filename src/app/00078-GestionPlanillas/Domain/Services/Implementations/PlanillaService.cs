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
        public List<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores(int? anio, int? mes, int? idCategoria)
        {
            var lista = VW_ResumenPlanillaTrabajador.FindAll(anio, mes, idCategoria)
                .Select(x => Mapper.VW_ResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(x)).ToList();

            return lista;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int anio, int mes, int categoriaPlanillaID, int userID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("I_TrabajadorID");

            trabajadores.ForEach(x => {
                dataTable.Rows.Add(x);
            });

            var generarPlanilla = new USP_I_GenerarPlanilla_Docente_Administrativo()
            {
                Tbl_Trabajador = dataTable,
                I_Anio = anio,
                I_Mes = mes,
                I_CategoriaPlanillaID = categoriaPlanillaID,
                I_UserID = userID
            };

            var result = generarPlanilla.Execute();

            return Mapper.Result_To_Response(result);
        }

        public bool ExistePlanillaTrabajador(int idTrabajador, int idPeriodo, int idCategoria)
        {
            try
            {
                var view = VW_ResumenPlanillaTrabajador.GetByPeriodoTrabajador(idTrabajador, idPeriodo, idCategoria);

                bool existePlanilla = (view != null ? true : false);

                return existePlanilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
