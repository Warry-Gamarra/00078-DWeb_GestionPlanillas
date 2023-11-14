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
        IEnumerable<ResumenPlanillaTrabajadorDTO> ListarResumenPlanillaTrabajadores(int año, int mes, int idCategoria);

        Response GenerarPlanilla(List<int> trabajadores, int año, int mes, int categoriaPlanillaID, int userID);

        bool ExistePlanillaTrabajador(int idTrabajador, int año, int mes, int idCategoria);

        IEnumerable<TotalPlanillaDependenciaDTO> ListarTotalPlanillaPorDependencia(int año, int mes, int idCategoria);

        ResumenSIAFDTO ListarResumenSIAF(int año, int mes, int idCategoria);
    }
}
