﻿using Domain.Entities;
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

        Response GenerarPlanilla(int I_Anio, int I_Mes, int? I_CategoriaPlanillaID, int userID);
    }
}
