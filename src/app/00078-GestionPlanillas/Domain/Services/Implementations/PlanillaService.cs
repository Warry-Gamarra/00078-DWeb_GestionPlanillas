using Data.Procedures;
using Data.Views;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
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

        public Response GenerarPlanilla(int I_Anio, int I_Mes, int? I_CategoriaPlanillaID, int userID)
        {
            var generarPlanilla = new USP_I_GenerarPlanilla_Docente_Administrativo()
            {
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
