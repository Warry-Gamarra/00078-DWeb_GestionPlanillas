using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.ServiceFacade
{
    public interface IDependenciaServiceFacade
    {
        Response GrabarDependencia(Operacion operacion, DependenciaModel model, int userID);

        List<DependenciaModel> ListarDependencias();

        SelectList ObtenerComboDependencias(bool incluirDeshabilitados = false, int? selectedItem = null);

        DependenciaModel ObtenerDependencia(int dependenciaID);

        Response CambiarEstado(int dependenciaID, bool estaHabilitado, int userID);

        Response Eliminar(int dependenciaID, int userID);
    }
}
