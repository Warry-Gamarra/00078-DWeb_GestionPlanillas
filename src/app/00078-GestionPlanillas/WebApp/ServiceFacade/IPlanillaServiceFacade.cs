using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IPlanillaServiceFacade
    {
        List<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int? anio, int? mes, int? idCategoria);

        Response GenerarPlanilla(List<int> trabajadores, int anio, int mes, int categoriaPlanillaID, int userID);
    }
}
