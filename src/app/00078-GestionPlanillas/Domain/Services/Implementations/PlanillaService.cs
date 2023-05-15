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
        public List<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores()
        {
            var lista = VW_ResumenPlanillaTrabajador.FindAll()
                .Select(x => Mapper.VW_ResumenPlanillaTrabajador_To_ResumenPlanillaTrabajadorDTO(x)).ToList();

            return lista;
        }

        public Response GenerarPlanilla(List<int> trabajadores, int I_Anio, int I_Mes, int I_CategoriaPlanillaID, int userID)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("I_TrabajadorID");

            trabajadores.ForEach(x => {
                dataTable.Rows.Add(x);
            });

            var generarPlanilla = new USP_I_GenerarPlanilla_Docente_Administrativo()
            {
                Tbl_Trabajador = dataTable,
                I_Anio = I_Anio,
                I_Mes = I_Mes,
                I_CategoriaPlanillaID = I_CategoriaPlanillaID,
                I_UserID = userID
            };

            var result = generarPlanilla.Execute();

            return Mapper.Result_To_Response(result);
        }
    }
}
