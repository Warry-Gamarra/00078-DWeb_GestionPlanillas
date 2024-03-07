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
    public interface IGrupoTrabajoService
    {
        Response GrabarGrupoTrabajo(Operacion operacion, GrupoTrabajoEntity grupoTrabajoEntity, int userID);

        List<GrupoTrabajoDTO> ListarGruposTrabajo(bool incluirDeshabilitados = false);

        GrupoTrabajoDTO ObtenerGrupoTrabajo(int grupoTrabajoID);

        Response CambiarEstado(int grupoTrabajoID, bool estaHabilitado, int userID);

        Response Eliminar(int grupoTrabajoID, int userID);
    }
}
