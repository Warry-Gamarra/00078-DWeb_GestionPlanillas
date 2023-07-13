using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPlanillaService
    {
        List<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores();

        Response GenerarPlanilla(List<int> trabajadores, int anio, int mes, int categoriaPlanillaID, int userID);
    }
}
