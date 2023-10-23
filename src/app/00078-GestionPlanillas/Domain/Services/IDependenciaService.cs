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
    public interface IDependenciaService
    {
        Response GrabarDependencia(Operacion operacion, DependenciaEntity dependenciaEntity, int userID);

        List<DependenciaDTO> ListarDependencias(bool incluirDeshabilitados = false);

        DependenciaDTO ObtenerDependencia(int dependenciaID);

        Response CambiarEstado(int dependenciaID, bool estaHabilitado, int userID);

        Response Eliminar(int dependenciaID, int userID);
    }
}
