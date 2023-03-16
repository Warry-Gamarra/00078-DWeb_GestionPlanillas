using Data.Views;
using Domain.Entities;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
