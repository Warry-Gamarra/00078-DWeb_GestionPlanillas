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
        List<ResumenPlanillaTrabajadorModel> ListarResumenPlanillaTrabajador(int año, int mes, int idCategoria);

        Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID);

        List<TotalPlanillaDependenciaModel> ListarTotalPlanillaPorDependencia(int año, int mes, int idCategoria);

        ResumenSIAFModel ListarResumenSIAF(int año, int mes, int idCategoria);
    }
}
